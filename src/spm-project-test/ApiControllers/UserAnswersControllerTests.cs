using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
using SPM_Project.EntityModels;

namespace SPM_Project.ApiControllers.Tests
{
    public class UserAnswersControllerTests
    {

        private UOWMocker _uowMocker;

        private UserAnswersController _controller;

        private UserAnswer _testUserAns;


        private McqQuestion _testMcqQuestion;


        private TFQuestion _testTFQuestion;



        private UserAnswer TestUserAnswerCreator()
        {
            var uAns = new UserAnswer()
            {

            };

            typeof(UserAnswer).GetProperty(nameof(uAns.Id)).SetValue(uAns, 1);

            return uAns; 
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
            typeof(McqQuestion).GetProperty(nameof(val.Id)).DeclaringType.GetProperty(nameof(val.Id)).SetValue(val, 1);

            return val; 
        }



        public UserAnswersControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new UserAnswersController(_uowMocker.mockUnitOfWork.Object);
            _testUserAns = TestUserAnswerCreator();
            _testMcqQuestion = TestMcqQuestionCreator();
            _testTFQuestion = TestTFQuestionCreator();

        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }




        //[Fact()]
        //public void UserAnswersControllerTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetUserAnswerAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetUserAnswerDTOAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetUserAnswersAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}

        //[Fact()]
        //public void GetChapterDTOsAsyncTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}




        [Fact()]
        public void CheckAnswerTest()
        {

            //WHEN USER ANS IS CORRECT TF QUESTION -> marks of 5 is awarded to _testUserAns

            //setup 
            _testUserAns.Answer = "true";
            _testTFQuestion.Answer = "true"; 
            _testUserAns.QuizQuestion = _testTFQuestion;

            //act 
            _controller.CheckAnswer(_testUserAns);


            Assert.Equal(5, _testUserAns.Marks);


            //WHEN USER ANS IS WRONG TF QUESTION -> 0 marks for _testUserAns
            _testUserAns.Answer = "false";

            //act 
            _controller.CheckAnswer(_testUserAns);

            Assert.Equal(0, _testUserAns.Marks);


            //WHEN USER ANS IS CORRECT MCQ QUESTION SINGLE-> marks of 5 is awarded to _testUserAns

            //setup 
            _testUserAns.Answer = "1";
            _testMcqQuestion.Answer = "1";
            _testUserAns.QuizQuestion = _testMcqQuestion;

            //act 
            _controller.CheckAnswer(_testUserAns);

            Assert.Equal(5, _testUserAns.Marks);


            //WHEN USER ANS IS CORRECT MCQ QUESTION SINGLE -> marks of 5 is awarded to _testUserAns

            _testUserAns.Answer = "2";

            //act 
            _controller.CheckAnswer(_testUserAns);

            Assert.Equal(0, _testUserAns.Marks);





            //WHEN USER ANS IS CORRECT MCQ QUESTION MULTI-> marks of 5 is awarded to _testUserAns

            //setup 
            _testUserAns.Answer = "1,2";
            _testMcqQuestion.Answer = "1,2";
            _testMcqQuestion.IsMultiSelect = true;
            _testUserAns.QuizQuestion = _testMcqQuestion;

            //act 
            _controller.CheckAnswer(_testUserAns);

            Assert.Equal(5, _testUserAns.Marks);


            //WHEN USER ANS IS CORRECT MCQ QUESTION MULTI -> marks of 5 is awarded to _testUserAns

            _testUserAns.Answer = "2";

            //act 
            _controller.CheckAnswer(_testUserAns);

            Assert.Equal(0, _testUserAns.Marks);








        }
    }
}