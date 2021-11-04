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

    //TODO ADD TESTS TO RETREIVE QUIZZES 
    public class QuizzesController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;

        public CourseClassesController _courseClassesCon;

        public CoursesController _coursesCon;

        public ChaptersController _chaptersCon; 

        public QuizzesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _courseClassesCon = new CourseClassesController(unitOfWork);

            _coursesCon = new CoursesController(unitOfWork);

            _chaptersCon = new ChaptersController(unitOfWork); 
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

            var courseClass = await _courseClassesCon.GetCourseClassAsync(quizDTO.CourseClassId, "Chapters,GradedQuiz");


            //check the datetine of the class start if class has not started, throw badrequest
            if (!courseClass.IsCourseClassModifiable())
            {
                throw new BadRequestException("Course Class has already started; material can't be modified");
            }



            if (quizDTO.IsGraded)
            {
                courseClass.GradedQuiz = await ConvertQuizDTOToQuizAsync(quizDTO); 

            }

            else
            {
                var chap = await _chaptersCon.GetChapterAsync((int)quizDTO.ChapterId, "Quizzes");

                chap.Quizzes = new List<Quiz>();

                chap.Quizzes.Add(await ConvertQuizDTOToQuizAsync(quizDTO)); 

            }

            await _unitOfWork.CompleteAsync(); 
            return Ok(); 
        }


        [HttpDelete, Route("{id:int}", Name = "DeleteQuiz")]
        public async Task<IActionResult> DeleteQuizAPIAsync(int id)
        {
            //check if quiz exists 

            //TODO CREATE REUSABLE FUNCTION TO RETREIVE QUIZ
            var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(id, "Questions"); 

            if (quiz == null)
            {
                throw new NotFoundException($"Quiz of Id {id} does not exist"); 
            }

            await _unitOfWork.QuizRepository.RemoveByEntityAsync(quiz);
            await _unitOfWork.CompleteAsync();

            return Ok(); 
        }


        [HttpGet, Route("{id:int}", Name = "GetQuiz")]
        public async Task<IActionResult> GetQuizAPIAsync(int id)
        {
            //check if quiz exists 

            //TODO CREATE REUSABLE FUNCTION TO RETREIVE QUIZ
            var quiz = await GetQuizDTOAsync(id, "CourseClass,Chapter,Questions"); 

            if (quiz == null)
            {
                throw new NotFoundException($"Quiz of Id {id} does not exist");
            }


            return Ok(quiz);
        }



        //NON-API FUNCTIONS----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [NonAction]
        public Dictionary<string, string> ValidateQuizDTOInput(QuizDTO quizDTO)
        {
            Dictionary<string, string> inputErrors = new Dictionary<string, string>();


            if (!quizDTO.IsChapterIdProvided())
            {
                inputErrors.Add($"QuizDTO", "Please provide ChapterId for ungraded quizzes");
 
            }

            for (int i = 0; i < quizDTO.Questions.Count; i++)
            {

                if (!quizDTO.Questions[i].IsQuestionTypeProvided())
                {
                     inputErrors.Add($"Questions[{i}]" + ".QuestionType", $"Question types are {string.Join(",", QuizQuestion.Discriminators)}");

                }

                if (!quizDTO.Questions[i].IsAnswerFormatted())
                {
                    inputErrors.Add($"Questions[{i}]" + ".Answer", $"Please provide the proper format for the answer");
                }

            }

            return inputErrors;
        }


        [NonAction]
        public async Task<Quiz> ConvertQuizDTOToQuizAsync(QuizDTO quizDTO)    
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
                quiz.Chapter = await _chaptersCon.GetChapterAsync((int)quizDTO.ChapterId);
            };


            quiz.CourseClass = await _courseClassesCon.GetCourseClassAsync((int)quizDTO.CourseClassId);
     

            //TODO CHECK THIS
            if (quizDTO.Id != 0)
            {
                typeof(Quiz).GetProperty(nameof(quiz.Id)).SetValue(quiz, 1);
            }

            foreach (var item in quizDTO.Questions)
            {
                quiz.Questions.Add(ConvertQuizQuestionDTOToQuizQuestion(item)); 
            }

            return quiz; 

        }

        [NonAction]
        public QuizQuestion ConvertQuizQuestionDTOToQuizQuestion(QuizQuestionDTO quizQuestionDTO)
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


        [NonAction]
        public QuizDTO ConvertQuizToQuizDTO(Quiz quiz)
        {
            var quizDTO = new QuizDTO(quiz);

            //loop through questions to add to QuizDTO 

            if (quiz.Questions != null)
            {
                foreach (var item in quiz.Questions)
                {
                    if (item.QuestionType=="McqQuestion")
                    {
                        var ques = (McqQuestion)item;
                        quizDTO.Questions.Add(new QuizQuestionDTO(ques)); 

                    }
                    else
                    {
                        var ques = (TFQuestion)item;
                        quizDTO.Questions.Add(new QuizQuestionDTO(ques));
                    }
                }
            }

            return quizDTO; 

        }


        
        [NonAction]
        public async Task<Quiz> GetQuizAsync(int id, string properties = "")
        {
            var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(id, properties);

            if (quiz == null)
            {
                throw new NotFoundException($"Quiz of id {id} is not found");
            }
            return quiz;
        }

        [NonAction]
        public async Task<QuizDTO> GetQuizDTOAsync(int id, string properties = "")
        {
            var quiz = await GetQuizAsync(id, properties);


            return ConvertQuizToQuizDTO(quiz); 
        }






    }
}