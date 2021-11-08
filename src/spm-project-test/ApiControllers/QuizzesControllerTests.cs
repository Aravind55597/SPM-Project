using SPM_Project.ApiControllers;
using Moq;
using SPM_Project.DTOs;
using SPM_Project.EntityModels;
using SPM_ProjectTests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using SPM_Project.CustomExceptions;

namespace SPM_Project.ApiControllers.Tests
{
    
    //https://www.meziantou.net/quick-introduction-to-xunitdotnet.htm
    public class QuizzesControllerTests : IDisposable
    {
        private QuizzesController _controller;

        //private ChaptersController _chaptersController; 
        private UOWMocker _uowMocker;



        private QuizDTO _testQuizDTO;

        private QuizDTO _testQuizDTOGraded;

        private Dictionary<string, string> _errorDict;


        private Quiz _testQuiz;
        private Quiz _testQuizGraded;


        private McqQuestion _testMcqQuestion;

        private TFQuestion _testTFQuestion;




        //TEST DTO CLASSES-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private QuizDTO TestQuizDTOCreator(bool isGraded)
        {

            int? chapId = null;
            if (!isGraded)
            {
                chapId = 1;
            }
            return new QuizDTO()
            {
                Name = "Graded Quiz",
                Description = "Graded Quiz Description",
                IsGraded = isGraded,
                ChapterId = chapId,
                CourseClassId = 1,
                TimeLimit = 20,
                Questions = new List<QuizQuestionDTO>()
                {
                    //non-multi select mcq
                    new QuizQuestionDTO()
                    {
                        ImageUrl="https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                        Marks= 5,
                        Question= "Test-question",
                        QuestionType= "McqQuestion",
                        Answer= "1",
                        Option1= "test option 1",
                        Option2= "test option 2",
                        Option3= "test option 3",
                        Option4= "test option 4"
                    }
                    ,
                    //multi-select mcq
                    new QuizQuestionDTO()
                    {
                        ImageUrl="https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                        Marks= 5,
                        Question= "Test-question",
                        QuestionType= "McqQuestion",
                        Answer= "1,2",
                        Option1= "test option 1",
                        Option2= "test option 2",
                        Option3= "test option 3",
                        Option4= "test option 4",
                        IsMultiSelect=true
                    }
                    ,
                    //true false question
                    new QuizQuestionDTO()
                    {
                        ImageUrl="https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                        Marks= 5,
                        Question= "Test-question",
                        QuestionType= "TFQuestion",
                        Answer= "True",
                        TrueOption= "True Option",
                        FalseOption= "False Option",
                    }
                }
            };
        }




        //TEST ENTITY CLASSES-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private Chapter TestChapterCreator()
        {
            var chap = new Chapter();
            typeof(Chapter).GetProperty(nameof(chap.Id)).SetValue(chap, 1);
            return chap;
        }

        private CourseClass TestCourseClassCreator()
        {
            var courseClass = new CourseClass();
            typeof(CourseClass).GetProperty(nameof(courseClass.Id)).SetValue(courseClass, 1);
            return courseClass;
        }


        private Quiz TestQuizCreator(bool isGraded)
        {
            Chapter chap = null;
            if (!isGraded)
            {
                chap = TestChapterCreator();
            }
            var quiz = new Quiz()
            {
                Name = "Graded Quiz",
                Description = "Graded Quiz Description",
                IsGraded = isGraded,
                Chapter = chap,
                TimeLimit = 20,
                CourseClass = TestCourseClassCreator(),
                Questions = new List<QuizQuestion>()
                {
                    new McqQuestion()
                    {
                        ImageUrl="https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                        Question= "Test-question",
                        QuestionType= "McqQuestion",
                        Answer= "1",
                          Marks= 5,
                        Option1= "test option 1",
                        Option2= "test option 2",
                        Option3= "test option 3",
                        Option4= "test option 4"
                    },

                    new McqQuestion()
                    {
                        ImageUrl="https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                        Question= "Test-question",
                        QuestionType= "McqQuestion",
                        Answer= "1,2",
                        Marks= 5,
                        Option1= "test option 1",
                        Option2= "test option 2",
                        Option3= "test option 3",
                        Option4= "test option 4",
                        IsMultiSelect=true
                    },

                    new TFQuestion()
                    {
                        ImageUrl="https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                        Marks= 5,
                        Question= "Test-question",
                        QuestionType= "TFQuestion",
                        Answer= "True",
                        TrueOption= "True Option",
                        FalseOption= "False Option",

                    },

                }
            };

            return quiz;
        }

        private McqQuestion TestMcqQuestionCreator()
        {
            var val = new McqQuestion()
            {
                ImageUrl = "https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                Question = "Test-question",
                QuestionType = "McqQuestion",
                Marks = 5,
                Option1 = "test option 1",
                Option2 = "test option 2",
                Option3 = "test option 3",
                Option4 = "test option 4"
            };
            typeof(McqQuestion).GetProperty(nameof(val.Id)).DeclaringType.GetProperty(nameof(val.Id)).SetValue(val, 1);

            return val;
        }
        private TFQuestion TestTFQuestionCreator()
        {
            var val = new TFQuestion()
            {
                ImageUrl = "https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
                Marks = 5,
                Question = "Test-question",
                QuestionType = "TFQuestion",
                TrueOption = "True Option",
                FalseOption = "False Option",

            };
            typeof(TFQuestion).GetProperty(nameof(val.Id)).DeclaringType.GetProperty(nameof(val.Id)).SetValue(val, 1);

            return val;
        }

        //SET-UP------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public QuizzesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new QuizzesController(_uowMocker.mockUnitOfWork.Object);
            _testQuizDTO = TestQuizDTOCreator(false);
            _testQuizDTOGraded = TestQuizDTOCreator(true);
            _errorDict = new Dictionary<string, string>();
            _testQuiz = TestQuizCreator(false);
            _testQuizGraded = TestQuizCreator(true);

            _testMcqQuestion = TestMcqQuestionCreator();
            _testTFQuestion = TestTFQuestionCreator();


        }

        //TEAR-DOWN------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }


        [Fact()]
        //check that the graded & ungraded quiz are not equal
        public void QuizzesControllerTest_CheckIfObjectsAreEqual()
        {
            //GRADED & UNGRADED QUIZ ARE NOT EQUAL
            Assert.NotEqual(JsonSerializer.Serialize(_testQuiz), JsonSerializer.Serialize(_testQuizGraded));


            //GRADED & UNGRADED QUIZ DTO ARE UNEQUAL 
            Assert.NotEqual(JsonSerializer.Serialize(_testQuizDTO), JsonSerializer.Serialize(_testQuizDTOGraded));

        }


        [Fact()]

        public void ValidateQuizDTOInputTest_QuizAllCorrectInput_ReturnEmptyDictionary()
        {

            //WHEN ALL INPUTS ARE CORRECT -> EMPTY DICTIOANRY 

            //graded quiz
            Assert.Equal(JsonSerializer.Serialize(new Dictionary<string, string>()), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTOGraded)));

            //ungraded quiz
            Assert.Equal(JsonSerializer.Serialize(new Dictionary<string, string>()), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));

        }

        [Fact()]
        public void ValidateQuizDTOInputTest_UngradedQuizChapterIdNotProvided_ReturnErrorDict()
        {

            //CHAPTER ID NOT PROVIDED -> ERROR DICTIONARY 
            _errorDict.Add("QuizDTO", "Please provide ChapterId for ungraded quizzes");
            _testQuizDTO.ChapterId = null;
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));

        }

        [Fact()]
        public void ValidateQuizDTOInputTest_QuizQuestionTypeNotProvidedOrMalformed_ReturnErrorDict()
        {

            //1ST QUESTION QUESTIONTYPE IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 2
            _errorDict.Add("Questions[0].QuestionType", "Question types are TFQuestion,McqQuestion");
            _errorDict.Add("Questions[0].Answer", "Please provide the proper format for the answer");

            _testQuizDTO.Questions[0].QuestionType = "sdfsdf";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(2, _errorDict.Count);

            //1ST QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuizDTO.Questions[0].QuestionType = "";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(2, _errorDict.Count);


            //2ND QUESTION QUESTIONTYPE IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 4
            _errorDict.Add("Questions[1].QuestionType", "Question types are TFQuestion,McqQuestion");
            _errorDict.Add("Questions[1].Answer", "Please provide the proper format for the answer");

            _testQuizDTO.Questions[1].QuestionType = "sdfsdf";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(4, _errorDict.Count);

            //2ND QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 4
            _testQuizDTO.Questions[1].QuestionType = "";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(4, _errorDict.Count);

            //3RD QUESTION QUESTIONTYPE IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 6
            _errorDict.Add("Questions[2].QuestionType", "Question types are TFQuestion,McqQuestion");
            _errorDict.Add("Questions[2].Answer", "Please provide the proper format for the answer");

            _testQuizDTO.Questions[2].QuestionType = "sdfsdf";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(6, _errorDict.Count);

            //3RD QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 6
            _testQuizDTO.Questions[2].QuestionType = "";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(6, _errorDict.Count);


        }



        [Fact()]
        public void ValidateQuizDTOInputTest_QuizQuestionAnswerMalformedOrNotProvided_ReturnErrorDict()
        {
            //1ST QUESTION (MCQ SINGLE OPTION) ANSWER IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 1
            _errorDict.Add("Questions[0].Answer", "Please provide the proper format for the answer");

            _testQuizDTO.Questions[0].Answer = ";LIKUSDF;OASHDF";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Single(_errorDict);

            //1ST QUESTION ANSWER IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 1
            _testQuizDTO.Questions[0].Answer = "";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Single(_errorDict);

            //1ST QUESTION ANSWER  INTEGER IS SMALLER THAN OR BIGGER THAN 2 -> RETURN ERROR DICT -> LENGTH IS 1
            _testQuizDTO.Questions[0].Answer = "234";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Single(_errorDict);


            //2ND QUESTION (MCQ MULTI OPTION) ANSWER IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 2
            _errorDict.Add("Questions[1].Answer", "Please provide the proper format for the answer");

            _testQuizDTO.Questions[1].Answer = "sdfsdf";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(2, _errorDict.Count);

            //2ND QUESTION  (MCQ MULTI OPTION) ANSWER IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuizDTO.Questions[1].Answer = "";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(2, _errorDict.Count);


            //2ND QUESTION  (MCQ MULTI OPTION) ANSWER LENGTH IS GREATER THAN 4 -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuizDTO.Questions[1].Answer = "1,2,3,4,5";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(2, _errorDict.Count);

            //2ND QUESTION  (MCQ MULTI OPTION) ANSWER LENGTH IS LOWER THAN OR EQUAL TO 1 -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuizDTO.Questions[1].Answer = "1";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(2, _errorDict.Count);


            //3RD QUESTION (TRUE FALSE) ANSWER IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 3
            _errorDict.Add("Questions[2].Answer", "Please provide the proper format for the answer");

            _testQuizDTO.Questions[2].Answer = "sdfsdf";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(3, _errorDict.Count);

            //3RD QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 3
            _testQuizDTO.Questions[2].Answer = "";
            Assert.Equal(JsonSerializer.Serialize(_errorDict), JsonSerializer.Serialize(_controller.ValidateQuizDTOInput(_testQuizDTO)));
            Assert.Equal(3, _errorDict.Count);
        }


        [Fact()]
        public async Task ConvertQuizDTOToQuizTest()
        {


            //UNGRADED QUIZ -> ALL FIELDS OF QUIZ DTO IS FILLED

            //mock retreival of chapter by  GetChaptersAsync() function called by chapters controllers
            _uowMocker.mockChapterRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(TestChapterCreator()).Verifiable("When chapter exists : GetIdByAsync is not called");

            //mock retrival of course class by  GetCourseClassAsync() function called by course classes controller 
            _uowMocker.mockCourseClassRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(TestCourseClassCreator()).Verifiable("When course class exists : GetIdByAsync is not called");




            Assert.Equal(JsonSerializer.Serialize(_testQuiz), JsonSerializer.Serialize(await _controller.ConvertQuizDTOToQuizAsync(_testQuizDTO)));

            //GRADED QUIZ -> ALL FIELDS OF QUIZ DTO IS FILLED; NO CHAPTER ID PROVIDED -> CHAPTER IN QUIZ MUST BE NULL 

            //run ConvertQuizDTOToQuiz
            var result = await _controller.ConvertQuizDTOToQuizAsync(_testQuizDTOGraded);
            Assert.Equal(JsonSerializer.Serialize(TestQuizCreator(true)), JsonSerializer.Serialize(result));

            //check that Chapter property of Quiz object returned is null
            Assert.Null(result.Chapter);


        }

        [Fact()]
        public void ConvertQuizQuestionDTOToQuizQuestionTest()
        {

            //ALL TYPES OF QUESTIONS ARE WELL-FORMED

            //McqQuestion -> single
            Assert.Equal(JsonSerializer.Serialize(TestQuizCreator(true).Questions[0]), JsonSerializer.Serialize(_controller.ConvertQuizQuestionDTOToQuizQuestion(TestQuizDTOCreator(true).Questions[0])));


            //McqQuestion -> multi
            Assert.Equal(JsonSerializer.Serialize(TestQuizCreator(true).Questions[1]), JsonSerializer.Serialize(_controller.ConvertQuizQuestionDTOToQuizQuestion(TestQuizDTOCreator(true).Questions[1])));


            //TFQuestion
            Assert.Equal(JsonSerializer.Serialize(TestQuizCreator(true).Questions[2]), JsonSerializer.Serialize(_controller.ConvertQuizQuestionDTOToQuizQuestion(TestQuizDTOCreator(true).Questions[2])));



        }




        [Fact()]
        public void ConvertQuizToQuizDTOTest()
        {
            //Ungraded quiz
            Assert.Equal(JsonSerializer.Serialize(_testQuizDTO), JsonSerializer.Serialize(_controller.ConvertQuizToQuizDTO(_testQuiz)));

            //graded quiz 
            Assert.Equal(JsonSerializer.Serialize(_testQuizDTOGraded), JsonSerializer.Serialize(_controller.ConvertQuizToQuizDTO(_testQuizGraded)));
        }


        [Fact()]
        public async Task GetQuizAsyncTest()
        {

            //WHEN QUIZ EXISTS -> GRADED

            //mock retrival of quiz by  GetByIdAsync function called by Quiz Repository
            _uowMocker.mockQuizRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(_testQuiz).Verifiable("GetIdByAsync is not called");




            Assert.Equal(JsonSerializer.Serialize(_testQuiz), JsonSerializer.Serialize(await _controller.GetQuizAsync(1)));

            //verify getidbyasync is called 
            _uowMocker.mockQuizRepository
            .Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));



            //WHEN QUIZ EXISTS -> UNGRADED

            //mock retrival of quiz by  GetByIdAsync function called by Quiz Repository
            _uowMocker.mockQuizRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(_testQuizGraded).Verifiable("GetIdByAsync is not called");



            Assert.Equal(JsonSerializer.Serialize(_testQuizGraded), JsonSerializer.Serialize(await _controller.GetQuizAsync(1)));

            //verify getidbyasync is called 
            _uowMocker.mockQuizRepository
            .Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));

            //WHEN QUIZ DOES NOT EXIST -> THROW NOT FOUND 

            //mock retrival of quiz by  GetByIdAsync function called by Quiz Repository -> return null
            _uowMocker.mockQuizRepository
            .Setup(l => l.GetByIdAsync(0, It.IsAny<string>()))
            .ReturnsAsync((Quiz)null).Verifiable("GetIdByAsync is not called");


            Func<Task> action = (async () => await _controller.GetQuizAsync(2, ""));
            ////check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);

            //verify getidbyasync is called 
            _uowMocker.mockQuizRepository
            .Verify(l => l.GetByIdAsync(2, It.IsAny<string>()));


        }



        [Fact()]
        public async Task GetQuizQuestionAsyncTest()
        {
            //RETREIVE MCQ QUESTION 

            //mock retrival of quiz Question by  GetByIdAsync function called by Quiz Question Repository
            _uowMocker.mockQuizQuestionRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(_testMcqQuestion).Verifiable("GetIdByAsync is not called");



            Assert.Equal(JsonSerializer.Serialize(_testMcqQuestion), JsonSerializer.Serialize((McqQuestion)await _controller.GetQuizQuestionAsync(1)));

            //verify get  id by async is called 
            _uowMocker.mockQuizQuestionRepository
            .Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));



            //RETREIVE TF QUESTION 


            //mock retrival of quiz Question by  GetByIdAsync function called by Quiz Question Repository
            _uowMocker.mockQuizQuestionRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(_testTFQuestion).Verifiable("GetIdByAsync is not called");



            Assert.Equal(JsonSerializer.Serialize(_testTFQuestion), JsonSerializer.Serialize((TFQuestion)await _controller.GetQuizQuestionAsync(1)));

            //verify get  id by async is called 
            _uowMocker.mockQuizQuestionRepository
            .Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));



            //NO QUESTIONS RETREIVED -> throw not found 



            //mock retrival of quiz by  GetByIdAsync function called by Quiz Repository -> return null
            _uowMocker.mockQuizQuestionRepository
            .Setup(l => l.GetByIdAsync(0, It.IsAny<string>()))
            .ReturnsAsync((QuizQuestion)null).Verifiable("GetIdByAsync is not called");


            Func<Task> action = (async () => await _controller.GetQuizQuestionAsync(0, ""));
            ////check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);

        }



    }
}