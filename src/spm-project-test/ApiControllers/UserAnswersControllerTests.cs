using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
using SPM_Project.EntityModels;
using Moq;
using SPM_Project.CustomExceptions;
using System.Linq.Expressions;
using SPM_Project.DTOs;
using System.Text.Json;

namespace SPM_Project.ApiControllers.Tests
{
    public class UserAnswersControllerTests : IDisposable
    {

        private UOWMocker _uowMocker;

        private UserAnswersController _controller;

        private UserAnswer _testUserAns;

        private List<UserAnswer> _testUserAnsList;


        private McqQuestion _testMcqQuestion;


        private TFQuestion _testTFQuestion;


        private Quiz _testQuiz;

        private UserAnswerDTO _testUserAnsDTO;

        private LMSUser _testUser; 


        private Mock<CourseClassesController> _mockCourseClassesCon;

        private Mock<UsersController> _mockUsersCon;


        private Mock<QuizzesController> _mockQuizzesCon;

        private UserAnswer TestUserAnswerCreator()
        {
            var uAns = new UserAnswer()
            {

            };

            typeof(UserAnswer).GetProperty(nameof(uAns.Id)).SetValue(uAns, 1);

            return uAns;
        }

        private Quiz TestQuizCreator()
        {
            var q = new Quiz()
            {

            };
            typeof(Quiz).GetProperty(nameof(q.Id)).SetValue(q, 1);

            return q;
        }


        private LMSUser TestUserCreator()
        {
            var q = new LMSUser()
            {

            };
            typeof(LMSUser).GetProperty(nameof(q.Id)).SetValue(q, 1);

            return q;
        }


        private List<UserAnswer> TestUserAnswerListCreator()
        {
            List<UserAnswer> answers = new List<UserAnswer>();

            for (int i = 0; i < 3; i++)
            {
                var uAns = new UserAnswer()
                {

                };

                typeof(UserAnswer).GetProperty(nameof(uAns.Id)).SetValue(uAns, i);
                answers.Add(uAns);
            }


            return answers;
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

        private UserAnswerDTO TestUserAnswerDTO()
        {
            return new UserAnswerDTO()
            {
                Id = 1,
                QuestionId = 1,
                Marks = 5,
            };
        }

        public UserAnswersControllerTests()
        {

            //mocks
            _uowMocker = new UOWMocker();
            _mockUsersCon = new Mock<UsersController>(null);
            _mockCourseClassesCon = new Mock<CourseClassesController>(null);
            _mockQuizzesCon = new Mock<QuizzesController>(null);


            //retrive userId of 1 
            _mockUsersCon.Setup(u => u.GetCurrentUserId()).ReturnsAsync(1).Verifiable("User Id WAS NOT retreived");

            //return test user answer when id of 1 is passed
            _uowMocker.mockUserAnswerRepository.Setup(l => l.GetByIdAsync(1, It.IsAny<string>())).ReturnsAsync(_testUserAns).Verifiable("GetByIdAsync  was not called");


            //test objects
            _testUserAns = TestUserAnswerCreator();
            _testMcqQuestion = TestMcqQuestionCreator();
            _testTFQuestion = TestTFQuestionCreator();
            _testUserAnsList = TestUserAnswerListCreator();
            _testQuiz = TestQuizCreator();
            _testUserAnsDTO = TestUserAnswerDTO();
            _testUser = TestUserCreator();

            //controller tested 
            _controller = new UserAnswersController(_uowMocker.mockUnitOfWork.Object);
            //set the controllers used within  as mocks 
            _controller._quizzesCon = _mockQuizzesCon.Object;
            _controller._usersCon = _mockUsersCon.Object;

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

        [Fact()]
        public async Task GetUserAnswerAsyncTest()
        {



            ////WHEN USER ANSWER EXISTS---------------------------------------------------------------------------------------------------------------------------------------- 

            //setup UserAnswer reposiotry to return a quiz answer object 
            _uowMocker.mockUserAnswerRepository.Setup(l => l.GetByIdAsync(1, It.IsAny<string>())).ReturnsAsync(_testUserAns).Verifiable("GetByIdAsyncWasNotCalled");

            var result = await _controller.GetUserAnswerAsync(1, "");
            ////check getidbyasync is called 
            _uowMocker.mockUserAnswerRepository.Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));
            ////check that user answer repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.UserAnswerRepository);
            ////check that the type of the result is of type user answer
            Assert.IsType<UserAnswer>(result);
            ////check that the result returned is the same as the test user answer
            Assert.Equal(JsonSerializer.Serialize(_testUserAns), JsonSerializer.Serialize(result));

            ////WHEN USER ANSWER DOES NOT EXIST---------------------------------------------------------------------------------------------------------------------------------------- 

            //setup UserAnswer reposiotry to return a quiz answer object 
            _uowMocker.mockUserAnswerRepository.Setup(l => l.GetByIdAsync(1, It.IsAny<string>())).ReturnsAsync((UserAnswer)null).Verifiable("GetByIdAsyncWasNotCalled");



            Func<Task> action = (async () => await _controller.GetUserAnswerAsync(2, ""));
            ////check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);
            ////check getidbyasync is called 
            _uowMocker.mockUserAnswerRepository.Verify(l => l.GetByIdAsync(2, It.IsAny<string>()));
            ////check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.UserAnswerRepository);
        }

        [Fact()]
        public async Task GetUserAnswerDTOAsyncTest()
        {

            //ANS IS FOR TF QUESTION -> Domain entity converted to DTO obejct 
            _testUserAns.Answer = "true";
            _testUserAns.Marks = 5;
            _testTFQuestion.Answer = "true";
            _testUserAns.QuizQuestion = _testTFQuestion;

            _testUserAnsDTO.Answer = "true";
            _testUserAnsDTO.Marks = 5;

            _uowMocker.mockUserAnswerRepository.Setup(l => l.GetByIdAsync(1, It.IsAny<string>())).ReturnsAsync(_testUserAns).Verifiable("GetByIdAsync  was not called");

            Assert.Equal(JsonSerializer.Serialize(_testUserAnsDTO), JsonSerializer.Serialize(await _controller.GetUserAnswerDTOAsync(1)));



            //ANS IS FOR MCQ SINGLE -> Domain entity converted to DTO obejct 
            _testUserAns.Answer = "1";
            _testMcqQuestion.Answer = "1";
            _testUserAns.QuizQuestion = _testMcqQuestion;

            _testUserAnsDTO.Answer = "1";

            _uowMocker.mockUserAnswerRepository.Setup(l => l.GetByIdAsync(1, It.IsAny<string>())).ReturnsAsync(_testUserAns).Verifiable("GetByIdAsync  was not called");

            Assert.Equal(JsonSerializer.Serialize(_testUserAnsDTO), JsonSerializer.Serialize(await _controller.GetUserAnswerDTOAsync(1)));


            //ANS IS FOR MCQ MULTI -> Domain entity converted to DTO obejct 
            _testUserAns.Answer = "1,2";
            _testMcqQuestion.Answer = "1,2";
            _testMcqQuestion.IsMultiSelect = true;
            _testUserAns.QuizQuestion = _testMcqQuestion;

            _testUserAnsDTO.Answer = "1,2";

            _uowMocker.mockUserAnswerRepository.Setup(l => l.GetByIdAsync(1, It.IsAny<string>())).ReturnsAsync(_testUserAns).Verifiable("GetByIdAsync  was not called");

            Assert.Equal(JsonSerializer.Serialize(_testUserAnsDTO), JsonSerializer.Serialize(await _controller.GetUserAnswerDTOAsync(1)));


        }

        [Fact()]
        public async Task GetUserAnswersAsyncTest()
        {

            // QUIZ EXISTS -> LIST OF USER ANSWERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 

            _mockQuizzesCon.Setup(q => q.GetQuizAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_testQuiz).Verifiable("GetQuizAsync is not called");

            //List of user answers returned 
            _uowMocker.mockUserAnswerRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<UserAnswer, bool>>>(), It.IsAny<Func<IQueryable<UserAnswer>, IOrderedQueryable<UserAnswer>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(_testUserAnsList).Verifiable("Quiz exist : GetAllAsync() is not called to retreive a list of user answers");
            Assert.Equal(_testQuiz, await _mockQuizzesCon.Object.GetQuizAsync(1));
            var result = await _controller.GetUserAnswersAsync(1, "");



            //check GetAllAsync is called 
            _uowMocker.mockUserAnswerRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<UserAnswer, bool>>>(), It.IsAny<Func<IQueryable<UserAnswer>, IOrderedQueryable<UserAnswer>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that user answer repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.UserAnswerRepository);
            //check that the type of the result is Chapter list
            Assert.IsType<List<UserAnswer>>(result);
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(_testUserAnsList), JsonSerializer.Serialize(result));


            //QUIZ EXISTS -> EMPTY LIST OF USER ANSWERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 

            //Empty user answers list is retunrned 
            _uowMocker.mockUserAnswerRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<UserAnswer, bool>>>(), It.IsAny<Func<IQueryable<UserAnswer>, IOrderedQueryable<UserAnswer>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<UserAnswer>()).Verifiable("Quiz exist : GetAllAsync() is not called to retreive a list of user answers");


            result = await _controller.GetUserAnswersAsync(1);

            //check GetAllAsync is called 
            _uowMocker.mockUserAnswerRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<UserAnswer, bool>>>(), It.IsAny<Func<IQueryable<UserAnswer>, IOrderedQueryable<UserAnswer>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.UserAnswerRepository);
            //check that the type of the result is Chapter list
            Assert.IsType<List<UserAnswer>>(result);
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(new List<UserAnswer>()), JsonSerializer.Serialize(result));


            //QUIZ DOES NOT EXIST---------------------------------------------------------------------------------------------------------------------------------------- 

            _mockQuizzesCon.Setup(q => q.GetQuizAsync(2, It.IsAny<string>())).ThrowsAsync(new NotFoundException()).Verifiable("GetQuizAsync is not called");

            Func<Task> action = (async () => await _controller.GetUserAnswersAsync(2));

            //check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);
            //verify that get quiz async was called 
            _mockQuizzesCon.Verify(q => q.GetQuizAsync(2, It.IsAny<string>()));


        }


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

        [Fact()]
        public async Task CreateConversionAsyncTest()
        {
            //TF QUESTION 

            //setup mocks
            _mockQuizzesCon.Setup(l => l.GetQuizQuestionAsync(1, It.IsAny<string>())).ReturnsAsync(_testTFQuestion).Verifiable("GetQuizAsync  was not called");
            _mockUsersCon.Setup(l => l.GetLMSUserAsync(1, It.IsAny<string>())).ReturnsAsync(_testUser).Verifiable("GetLMSUserAsync  was not called");

            //expected user answer -> 
            _testUserAns.User = _testUser;
            _testUserAns.QuizQuestion = _testTFQuestion;
            _testUserAns.Answer = "true";
                //change id to 0 since this is a creation processs
            typeof(UserAnswer).GetProperty(nameof(_testUserAns.Id)).SetValue(_testUserAns, 0);

            //change ans in dto 
            _testUserAnsDTO.Answer = "true";

            //user ans domain object is as expected 
            Assert.Equal(JsonSerializer.Serialize(_testUserAns), JsonSerializer.Serialize(await _controller.CreateConversionAsync(_testUserAnsDTO) ));

            //MCQ QUESTION SINGLE 

            //setup mocks 
            _mockQuizzesCon.Setup(l => l.GetQuizQuestionAsync(1, It.IsAny<string>())).ReturnsAsync(_testMcqQuestion).Verifiable("GetQuizAsync  was not called");

            //expected user answer -> 
            _testUserAns.QuizQuestion = _testMcqQuestion;
            _testUserAns.Answer = "1";
            //change ans in dto 
            _testUserAnsDTO.Answer = "1";

            //user ans domain object is as expected 
            Assert.Equal(JsonSerializer.Serialize(_testUserAns), JsonSerializer.Serialize(await _controller.CreateConversionAsync(_testUserAnsDTO)));



            //MCQ QUESTION MULTI

            //setup mocks 
            _mockQuizzesCon.Setup(l => l.GetQuizQuestionAsync(1, It.IsAny<string>())).ReturnsAsync(_testMcqQuestion).Verifiable("GetQuizAsync  was not called");

            //expected user answer -> 
            _testUserAns.QuizQuestion = _testMcqQuestion;
            _testUserAns.Answer = "1,2";
            //change ans in dto 
            _testUserAnsDTO.Answer = "1,2";

            //set mcq question to multi
            _testMcqQuestion.IsMultiSelect = true;
            //user ans domain object is as expected 
            Assert.Equal(JsonSerializer.Serialize(_testUserAns), JsonSerializer.Serialize(await _controller.CreateConversionAsync(_testUserAnsDTO)));

        }
    }
}