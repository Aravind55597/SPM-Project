using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs;
using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;

        public CourseClassesController _courseClassCon;

        public CoursesController _courseCon;

        public QuizzesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _courseClassCon = new CourseClassesController(unitOfWork);

            _courseCon = new CoursesController(unitOfWork);
        }

        //POST QUESTION-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //TODO UNIT TESTS

        [HttpPost, Route("", Name = "AddQuiz")]
        public async Task<IActionResult> PostQuizDTOAPIAsync([FromBody] QuizDTO quizDTO)
        {
            
            //Inputs based on complex input validation logic
            if (ValidateQuizDTOInput(quizDTO).Count!=0)
            {
                throw new BadRequestException("Inputs are not formatted corectly" , ValidateQuizDTOInput(quizDTO)); 
            }

            //check if courseclass exists 

            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(quizDTO.CourseClassId, "Chapters, GradedQuiz" ); 
            
            if (courseClass==null)
            {
                throw new NotFoundException($"Course Class of {quizDTO.CourseClassId} is not found");
            }

            //check the datetine of the class start if class has not started, throw badrequest
            if (!courseClass.IsCourseClassModifiable())
            {
                throw new BadRequestException("Course Class has already started; material can't be modified");
            }

 
            //check if  classes exists
            if (quizDTO.IsGraded)
            {
                if (courseClass.GradedQuiz!=null)
                {
                    throw new BadRequestException("Quiz already exists. Please update the graded quiz instead");
                }


                courseClass.GradedQuiz = new Quiz();
                courseClass.GradedQuiz = await ConvertQuizDTOToQuiz(quizDTO);
       
 
            }

            //check if chapter exists 
            else
            {
                var chap = await _unitOfWork.ChapterRepository.GetByIdAsync((int)quizDTO.ChapterId, "Quizzes");
                if (chap == null)
                {
                    throw new NotFoundException("Chapter does not exist");
                }
                if (chap.Quizzes==null)
                {
                    throw new BadRequestException("Quiz already exists. Please update the graded quiz instead");
                }

                chap.Quizzes = new List<Quiz>();

                chap.Quizzes.Add(await ConvertQuizDTOToQuiz(quizDTO)); 
            }

            await _unitOfWork.CompleteAsync(); 
            return Ok(); 
        }



        [NonAction]
        public Dictionary<string, string> ValidateQuizDTOInput(QuizDTO quizDTO)
        {
            Dictionary<string, string> inputErrors = new Dictionary<string, string>();

            IsChapterIdProvided(inputErrors, quizDTO);

            for (int i = 0; i < quizDTO.Questions.Count; i++)
            {
                IsQuestionTypeProvided(inputErrors, quizDTO.Questions[i], quizDTO, i);
                IsAnswerFormated(inputErrors, quizDTO.Questions[i], quizDTO, i);
            }

            return inputErrors;
        }



        [NonAction]
        public async Task<Quiz> ConvertQuizDTOToQuiz(QuizDTO quizDTO)
        {
            var quiz = new Quiz()
            {
                Name = quizDTO.Name,
                Description = quizDTO.Description,
                IsGraded = quizDTO.IsGraded,
                TimeLimit = quizDTO.TimeLimit,
                Questions = new List<QuizQuestion>()
            };

            //if not null , add chapter
            if (quizDTO.ChapterId != null)
            {
                quiz.Chapter = await _unitOfWork.ChapterRepository.GetByIdAsync((int)quizDTO.ChapterId);
            };


            //TODO CHECK THIS
            if (quizDTO.Id != 0)
            {
                typeof(Quiz).GetProperty(nameof(quiz.Id)).SetValue(quiz, 1);
            }

            foreach (var item in quizDTO.Questions)
            {
                quiz.Questions.Add(ConvertQuizQuestionDTOToQuiz(item)); 
            }

            return quiz; 

        }

        [NonAction]
        public QuizQuestion ConvertQuizQuestionDTOToQuiz(QuizQuestionDTO quizQuestionDTO)
        {
            if (quizQuestionDTO.QuestionType== "McqQuestion")
            {
                var mcq = new McqQuestion()
                {
                    ImageUrl= quizQuestionDTO.ImageUrl,
                    Question = quizQuestionDTO.Question,
                    QuestionType =quizQuestionDTO.QuestionType,
                    Marks = quizQuestionDTO.Marks,
                    Option1= quizQuestionDTO.Option1,
                    Option2=quizQuestionDTO.Option2,
                    Option3=quizQuestionDTO.Option3,
                    Option4=quizQuestionDTO.Option4,
                    IsMultiSelect=quizQuestionDTO.IsMultiSelect
                };

                mcq.SetAnswer(new List<int>().CommaSepStringToIntList(quizQuestionDTO.Answer));
                //typeof(McqQuestion).GetProperty(nameof(mcq.Id)).SetValue(mcq, 1, null);
                //TODO CHECK THIS

                if (quizQuestionDTO.Id !=0)
                {
                    typeof(McqQuestion).GetProperty(nameof(mcq.Id)).DeclaringType.GetProperty(nameof(mcq.Id)).SetValue(mcq, quizQuestionDTO.Id);
                }
                
                return mcq; 
            }
            else
            {
                var tf = new TFQuestion()
                {
                    ImageUrl = quizQuestionDTO.ImageUrl,
                    Question = quizQuestionDTO.Question,
                    QuestionType = quizQuestionDTO.QuestionType,
                    Marks = quizQuestionDTO.Marks,
                    TrueOption=quizQuestionDTO.TrueOption,
                    FalseOption=quizQuestionDTO.FalseOption
                };
                tf.SetAnswer(bool.Parse(quizQuestionDTO.Answer));
                
                if (quizQuestionDTO.Id != 0)
                {
                    typeof(TFQuestion).GetProperty(nameof(tf.Id)).DeclaringType.GetProperty(nameof(tf.Id)).SetValue(tf, quizQuestionDTO.Id);
                }
               
                return  tf;
            }

        }



        //PRIVATE METHODS------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [NonAction]
        private void IsQuestionTypeProvided(Dictionary<string, string> inputErrors, QuizQuestionDTO questionDTO, QuizDTO quizDTO, int index)
        {
            if (!QuizQuestion.Discriminators.Contains(questionDTO.QuestionType))
            {
                inputErrors.Add($"Questions[{index}]" + nameof(questionDTO.QuestionType), $"Question types are  {string.Join(",", QuizQuestion.Discriminators)}");
            }
        }
        [NonAction]
        private void IsAnswerFormated(Dictionary<string, string> inputErrors, QuizQuestionDTO questionDTO, QuizDTO quizDTO, int index)
        {
            if (QuizQuestion.Discriminators.Contains(questionDTO.QuestionType))
            {
                bool flag;
                switch (questionDTO.QuestionType)
                {
                    case "TFQuestion":

                        if (!Boolean.TryParse(questionDTO.Answer, out flag))
                        {
                            inputErrors.Add($"Questions[{index}]" + nameof(questionDTO.Answer), $"Please provide the proper format for the answer for a True/False Question");
                        }
                        break;

                    case "McqQuestion":

                        if (!new List<int>().CommaSepStringToIntListValidator(questionDTO.Answer))
                        {
                            inputErrors.Add($"Questions[{index}]" + nameof(questionDTO.Answer), $"Please provide the proper format for the answer for a MCQ Question ");
                        }

                        break;
                }
            }
        }

        [NonAction]
        private void IsChapterIdProvided(Dictionary<string, string> inputErrors, QuizDTO quizDTO)
        {
            if (!quizDTO.IsGraded && quizDTO.ChapterId == null)
            {

                inputErrors.Add($"QuizDTO", "Please provide marks for graded quizzes");

            }

        }


    }
}