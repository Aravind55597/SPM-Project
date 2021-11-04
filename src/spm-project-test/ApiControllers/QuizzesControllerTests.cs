using SPM_Project.ApiControllers;
using Moq;
using SPM_Project.DTOs;
using SPM_Project.EntityModels;
using SPM_ProjectTests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SPM_Project.ApiControllers.Tests
{
    //TODO ASSERT THE EXCEPTION MESSAGE
    //https://www.meziantou.net/quick-introduction-to-xunitdotnet.htm
    public class QuizzesControllerTests : IDisposable
    {
        private QuizzesController _controller;

        //private ChaptersController _chaptersController; 
        private UOWMocker _uowMocker;



        private QuizDTO _testQuiz;


        private Dictionary<string, string> _errorDict;




        //TEST DTO CLASSES-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private QuizDTO QuizDTOCreator(bool isGraded)
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




        //SET-UP------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public QuizzesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new QuizzesController(_uowMocker.mockUnitOfWork.Object);
            _testQuiz = QuizDTOCreator(false);
            _errorDict = new Dictionary<string, string>();

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

            var ungraded = _testQuiz.IsGraded = false;
            var graded = _testQuiz.IsGraded = true;
            Assert.NotEqual(Newtonsoft.Json.JsonConvert.SerializeObject(ungraded), Newtonsoft.Json.JsonConvert.SerializeObject(graded));
        }


        [Fact()]

        public void ValidateQuizDTOInputTest_QuizAllCorrectInput_ReturnEmptyDictionary()
        {

            //WHEN ALL INPUTS ARE CORRECT -> EMPTY DICTIOANRY 
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, string>()), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));

        }

        [Fact()]
        public void ValidateQuizDTOInputTest_UngradedQuizChapterIdNotProvided_ReturnErrorDict()
        {

            //CHAPTER ID NOT PROVIDED -> ERROR DICTIONARY 
            _errorDict.Add("QuizDTO", "Please provide ChapterId for ungraded quizzes");
            _testQuiz.ChapterId = null;
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));

        }

        [Fact()]
        public void ValidateQuizDTOInputTest_QuizQuestionTypeNotProvidedOrMalformed_ReturnErrorDict()
        {

            //1ST QUESTION QUESTIONTYPE IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 2
            _errorDict.Add("Questions[0].QuestionType", "Question types are TFQuestion,McqQuestion");
            _errorDict.Add("Questions[0].Answer", "Please provide the proper format for the answer");

            _testQuiz.Questions[0].QuestionType = "sdfsdf";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(2, _errorDict.Count);

            //1ST QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuiz.Questions[0].QuestionType = "";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(2, _errorDict.Count);


            //2ND QUESTION QUESTIONTYPE IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 4
            _errorDict.Add("Questions[1].QuestionType", "Question types are TFQuestion,McqQuestion");
            _errorDict.Add("Questions[1].Answer", "Please provide the proper format for the answer");

            _testQuiz.Questions[1].QuestionType = "sdfsdf";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(4, _errorDict.Count);

            //2ND QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 4
            _testQuiz.Questions[1].QuestionType = "";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(4, _errorDict.Count);

            //3RD QUESTION QUESTIONTYPE IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 6
            _errorDict.Add("Questions[2].QuestionType", "Question types are TFQuestion,McqQuestion");
            _errorDict.Add("Questions[2].Answer", "Please provide the proper format for the answer");

            _testQuiz.Questions[2].QuestionType = "sdfsdf";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(6, _errorDict.Count);

            //3RD QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 6
            _testQuiz.Questions[2].QuestionType = "";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(6, _errorDict.Count);


        }



        [Fact()]
        public void ValidateQuizDTOInputTest_QuizQuestionAnswerMalformedOrNotProvided_ReturnErrorDict()
        {
            //1ST QUESTION (MCQ SINGLE OPTION) ANSWER IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 1
            _errorDict.Add("Questions[0].Answer", "Please provide the proper format for the answer");

            _testQuiz.Questions[0].Answer = ";LIKUSDF;OASHDF";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(1, _errorDict.Count);

            //1ST QUESTION ANSWER IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 1
            _testQuiz.Questions[0].Answer = "";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(1, _errorDict.Count);

            //1ST QUESTION ANSWER  INTEGER IS SMALLER THAN OR BIGGER THAN 2 -> RETURN ERROR DICT -> LENGTH IS 1
            _testQuiz.Questions[0].Answer = "234";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(1, _errorDict.Count);


            //2ND QUESTION (MCQ MULTI OPTION) ANSWER IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 2
            _errorDict.Add("Questions[1].Answer", "Please provide the proper format for the answer");

            _testQuiz.Questions[1].Answer = "sdfsdf";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(2, _errorDict.Count);

            //2ND QUESTION  (MCQ MULTI OPTION) ANSWER IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuiz.Questions[1].Answer = "";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(2, _errorDict.Count);


            //2ND QUESTION  (MCQ MULTI OPTION) ANSWER LENGTH IS GREATER THAN 4 -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuiz.Questions[1].Answer = "1,2,3,4,5";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(2, _errorDict.Count);

            //2ND QUESTION  (MCQ MULTI OPTION) ANSWER LENGTH IS LOWER THAN OR EQUAL TO 1 -> RETURN ERROR DICT -> LENGTH IS 2
            _testQuiz.Questions[1].Answer = "1";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(2, _errorDict.Count);


            //3RD QUESTION (TRUE FALSE) ANSWER IS MALFORMED -> RETURN ERROR DICT -> LENGTH IS 3
            _errorDict.Add("Questions[2].Answer", "Please provide the proper format for the answer");

            _testQuiz.Questions[2].Answer = "sdfsdf";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
            Assert.Equal(3, _errorDict.Count);

            //3RD QUESTION QUESTIONTYPE IS NOT PROVIDED -> RETURN ERROR DICT -> LENGTH IS 3
            _testQuiz.Questions[2].Answer = "";
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(_errorDict), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ValidateQuizDTOInput(_testQuiz)));
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

            //mock retrival of course clas sby  GetCourseClassAsync() function called by course classes controller 
            _uowMocker.mockCourseClassRepository
            .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
            .ReturnsAsync(TestCourseClassCreator()).Verifiable("When course class exists : GetIdByAsync is not called");

            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestQuizCreator(false)), Newtonsoft.Json.JsonConvert.SerializeObject(await _controller.ConvertQuizDTOToQuizAsync(QuizDTOCreator(false))));

            //GRADED QUIZ -> ALL FIELDS OF QUIZ DTO IS FILLED; NO CHAPTER ID PROVIDED -> CHAPTER IN QUIZ MUST BE NULL 

            //run ConvertQuizDTOToQuiz
            var result = await _controller.ConvertQuizDTOToQuizAsync(QuizDTOCreator(true));
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestQuizCreator(true)), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //check that Chapter property of Quiz object returned is null
            Assert.Null(result.Chapter);


        }

        [Fact()]
        public void ConvertQuizQuestionDTOToQuizQuestionTest()
        {

            //ALL TYPES OF QUESTIONS ARE WELL-FORMED

            //McqQuestion -> single
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestQuizCreator(true).Questions[0]), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ConvertQuizQuestionDTOToQuizQuestion(QuizDTOCreator(true).Questions[0])));


            //McqQuestion -> multi
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestQuizCreator(true).Questions[1]), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ConvertQuizQuestionDTOToQuizQuestion(QuizDTOCreator(true).Questions[1])));


            //TFQuestion
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestQuizCreator(true).Questions[2]), Newtonsoft.Json.JsonConvert.SerializeObject(_controller.ConvertQuizQuestionDTOToQuizQuestion(QuizDTOCreator(true).Questions[2])));



        }


        //TODO TESTS FOR NON-API FUNCTIONS

        //[Fact()]
        //public void ConvertQuizToQuizDTOTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetQuizAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetQuizDTOAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetQuizQuestionAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetQuizQuestionsAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}


    }
}