using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.DataTableModels.DataTableData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SPM_Project.CustomExceptions;
using SPM_Project.EntityModels;
using Newtonsoft.Json;
using SPM_Project.DTOs;
using System.Linq.Expressions;
using SPM_Project.DTOs.RRModels;

namespace SPM_Project.ApiControllers.Tests
{
    public class CourseClassesControllerTests
    {

        private CourseClassesController _controller;
        private UOWMocker _uowMocker;

        private DTParameterModel _inputDTModel;

        private DTResponse<CourseClassTableData> _outputDTModel;


        public CourseClass ReturnCC()
        {
            return TestCourseClassCreator();
        }



        //returns a list of courseclasses 
        private List<CourseClass> TestCourseClassList()
        {
            List<CourseClass> courseClassesList = new List<CourseClass>();

            for (int i = 0; i < 10; i++)
            {
                courseClassesList.Add(TestCourseClassCreator());
            }

            return courseClassesList;
        }


        //createa test course class with random integer for tests 
        private CourseClass TestCourseClassCreator()
        {

            Random rnd = new Random();
            int id = rnd.Next(1, 50);

            var courseClass = new CourseClass()
            {


                Name = $"Test Course Class {id}",
                StartRegistration = DateTime.Now,
                EndRegistration = DateTime.Now,
                StartClass = DateTime.Now,
                EndClass = DateTime.Now,
                ClassTrainer = new LMSUser()
                {
                    Name = $"Test Trainer {id}",
                    Department = Department.Human_Resource,
                    DOB = DateTime.Now,

                },
                Course = new Course()
                {
                    Name = $"Test Course {1}",
                    Description = "Test Description",
                    PassingPercentage = (decimal)0.85
                },
                Slots = 30

            };

            //set id of the courseClass 
            typeof(CourseClass).GetProperty(nameof(courseClass.Id)).SetValue(courseClass, id);

            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(courseClass.ClassTrainer.Id)).SetValue(courseClass.ClassTrainer, id);

            //set id of course 
            typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, 1);

            return courseClass;

        }


        //sample test course class 
        private CourseClass TestCourseClass()
        {


            var courseClass = new CourseClass()
            {


                Name = $"Test Course Class {1}",
                StartRegistration = DateTime.Now,
                EndRegistration = DateTime.Now,
                StartClass = DateTime.Now,
                EndClass = DateTime.Now,
                ClassTrainer = new LMSUser()
                {
                    Name = $"Test Trainer {1}",
                    Department = Department.Human_Resource,
                    DOB = DateTime.Now,

                },
                Course = new Course
                {
                    Name = $"Test Course {1}",
                    Description = "Test Description",
                    PassingPercentage = (decimal)0.85
                },
                Slots = 30

            };

            //set id of the courseClass 
            typeof(CourseClass).GetProperty(nameof(courseClass.Id)).SetValue(courseClass, 1);

            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(courseClass.ClassTrainer.Id)).SetValue(courseClass.ClassTrainer, 1);

            //set id of course 
            typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, 1);

            return courseClass;

        }


        //sample TestCourse
        private Course TestCourse()
        {
            var course = new Course
            {
                Name = $"Test Course {1}",
                Description = "Test Description",
                PassingPercentage = (decimal)0.85
            };
            //set id of course 
            typeof(Course).GetProperty(nameof(course.Id)).SetValue(course, 1);

            return course;
        }




        public CourseClassesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new CourseClassesController(_uowMocker.mockUnitOfWork.Object);

            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<CourseClassTableData>();


            //return empty outPut model when GetCourseClassesDataTable() is called using courseClasses repo
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetCourseClassesDataTable(_inputDTModel, It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(_outputDTModel);


            //return testCourseClass when courseClass id of 1 is retreived 
            _uowMocker.mockCourseClassRepository.
                Setup(l => l.GetByIdAsync(1, "Course,ClassTrainer")).
                ReturnsAsync(TestCourseClass()).Verifiable("Course Class was NOT retreived");



            //when when course id = 1 , return course
            _uowMocker.mockCourseRepository
                .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync(TestCourse());




        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }



        //GetCourseClassesDTOAPIAsync-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_CourseExists_ReturnsOK()
        {


            //return classes when retreiving class that has the course of Id provided 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), It.IsAny<Func<IQueryable<CourseClass>, IOrderedQueryable<CourseClass>>>(), "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestCourseClassList()).Verifiable("Course Classes were not retreived");

            var result = await _controller.GetCourseClassesDTOAPIAsync(null, 1) as OkObjectResult;

            _uowMocker.mockCourseRepository.Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));


            _uowMocker.mockCourseClassRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), null, "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()));




            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //cCheck if the value is a response object 
            Assert.IsType<Response<List<CourseClassesDTO>>>(result.Value);

            var val = (Response<List<CourseClassesDTO>>)result.Value;
            //check if count is 10 
            Assert.Equal(10, val.Data.Count);
        }

        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_CourseDoesNotExists_ReturnsNotFoundException()
        {
            Func<Task> action = (async () => await _controller.GetCourseClassesDTOAPIAsync(null, 0));
            //check if ok is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);
        }



        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_RetreiveOneClass_ReturnsOK()
        {



            var result = await _controller.GetCourseClassesDTOAPIAsync(1, null) as OkObjectResult;

            _uowMocker.mockCourseClassRepository.Verify(l => l.GetByIdAsync(1, "Course,ClassTrainer"));


            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //cCheck if the value is a response object 
            Assert.IsType<Response<CourseClassesDTO>>(result.Value);

            var val = (Response<CourseClassesDTO>)result.Value;
            //check if course id is 1 
            Assert.Equal(1, val.Data.CourseId);
        }



        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_RetreiveAllClasses_ReturnsOK()
        {
            //return classes when retreiving class that has the course of Id provided 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), It.IsAny<Func<IQueryable<CourseClass>, IOrderedQueryable<CourseClass>>>(), "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestCourseClassList().Concat(TestCourseClassList()).ToList()).Verifiable("Course Classes were not retreived");



            var result = await _controller.GetCourseClassesDTOAPIAsync(null, null) as OkObjectResult;
            _uowMocker.mockCourseClassRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), It.IsAny<Func<IQueryable<CourseClass>, IOrderedQueryable<CourseClass>>>(), "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()));


            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //cCheck if the value is a response object 
            Assert.IsType<Response<List<CourseClassesDTO>>>(result.Value);

            var val = (Response<List<CourseClassesDTO>>)result.Value;
            //check if count is 10 
            Assert.Equal(20, val.Data.Count);

        }


        //GetCourseClassDTOAsync------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PASS



        [Fact()]
        public async Task GetCourseClassDTOAsync_ClassExists_ReturnsOneClass()
        {
            //assert that 1 class is returned 
            var result = await _controller.GetCourseClassDTOAsync(1);

            _uowMocker.mockCourseClassRepository.Verify(l => l.GetByIdAsync(1, "Course,ClassTrainer"));

            Assert.IsType<CourseClassesDTO>(result);

            Assert.Equal(1, result.Id);
        }


        [Fact()]
        public async Task GetCourseClassDTOAsync_ClassDoesNotExists_ThrowNotFoundException()
        {
            Func<Task> action = (async () => await _controller.GetCourseClassDTOAsync(0));

            await Assert.ThrowsAsync<NotFoundException>(action);

        }

        //GetCourseClassesDTOAsync-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Fact()]
        public async Task GetCourseClassesDTOAsync_CourseExists_ReturnsClasses()
        {


            //return classes when retreiving class that has the course of Id provided 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), null, "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestCourseClassList()).Verifiable("Course Classes were not retreived");



            //assert that 1 class is returned 
            var result = await _controller.GetCourseClassesDTOAsync(1);


            _uowMocker.mockCourseRepository.Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));
            _uowMocker.mockCourseClassRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), null, "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()));


            Assert.IsType<List<CourseClassesDTO>>(result);

            Assert.All(result,
                item => Assert.Contains("Test Trainer", item.TrainerName)

            );


            Assert.All(result,
            item => Assert.Equal(1, item.CourseId)


            );

        }


        [Fact()]
        public async Task GetCourseClassesDTOAsync_CourseDoesNotExist_NotFoundException()
        {

            Func<Task> action = async () => await _controller.GetCourseClassesDTOAsync(0);

            await Assert.ThrowsAsync<NotFoundException>(action);


        }



        [Fact()]
        public async Task GetCourseClassesDTOAsync_CourseExistsNoClasses_ReturnsEmptyList()
        {

            //return classes when retreiving class that has the course of Id provided 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), null, "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<CourseClass>()).Verifiable("Course Classes were not retreived");



            //assert that 1 class is returned 
            var result = await _controller.GetCourseClassesDTOAsync(1);


            _uowMocker.mockCourseRepository.Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));
            _uowMocker.mockCourseClassRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), null, "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()));


            Assert.IsType<List<CourseClassesDTO>>(result);

            Assert.Empty(result);

        }


        [Fact()]
        public async Task GetCourseClassesDTOAsync_NoCourseId_ReturnsAllClasses()
        {

            //return classes when retreiving class that has the course of Id provided 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), It.IsAny<Func<IQueryable<CourseClass>, IOrderedQueryable<CourseClass>>>(), "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestCourseClassList().Concat(TestCourseClassList()).ToList()).Verifiable("Course Classes were not retreived");


            var result = await _controller.GetCourseClassesDTOAsync(null);

            _uowMocker.mockCourseClassRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<CourseClass, bool>>>(), It.IsAny<Func<IQueryable<CourseClass>, IOrderedQueryable<CourseClass>>>(), "Course,ClassTrainer", It.IsAny<int>(), It.IsAny<int>()));

            Assert.IsType<List<CourseClassesDTO>>(result);

            Assert.All(result,
                item => Assert.Contains("Test Trainer", item.TrainerName)
            );

            Assert.All(result,
            item => Assert.Equal(1, item.CourseId)
            );

            Assert.Equal(20, result.Count);


        }



        //GetCourseClassesDataTable------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PASS

        //No User ID is passed 


        [Theory]
        [InlineData(1, null, true, false)]
        [InlineData(1, null, false, true)]
        [InlineData(1, null, true, true)]
        public async Task GetCourseClassesDataTableTest_NoLMSUserIdIsGiven_ReturnTableData(int? courseId, int? lmsUserId, bool isTrainer = false, bool isLearner = false)
        {

            //returns id of current user 
            _uowMocker.mockLMSUserRepository.Setup(l => l.RetrieveCurrentUserIdAsync()).ReturnsAsync(1).Verifiable("Id of the current user was NOT retreived");

            //returns a course (empty object)
            _uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Course()).Verifiable("Course is NOT retrieved");

            var result = await _controller.GetCourseClassesDataTable(_inputDTModel, courseId, lmsUserId, isTrainer, isLearner) as OkObjectResult;

            //verify that id of the current user is retreived 
            _uowMocker.mockLMSUserRepository.Verify(l => l.RetrieveCurrentUserIdAsync());
            //verify that course is retreived 
            _uowMocker.mockCourseRepository.Verify(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()));
            //verify that function returns a DTResposne form repostiory 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<DTResponse<CourseClassTableData>>(items);
            // Then

            Assert.IsType<DTResponse<CourseClassTableData>>(deserializedMessage);

        }

        //Course does not exist , function throws not found

        [Theory]
        [InlineData(1, null, true, false)]
        [InlineData(1, 1, false, true)]
        [InlineData(1, null, true, true)]
        public async Task GetCourseClassesDataTableTest_CourseDoesNotExist_ThrowNotFound(int? courseId, int? lmsUserId, bool isTrainer = false, bool isLearner = false)
        {

            //returns user based on the ID passed 
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new LMSUser()).Verifiable("GetByIdAsync LMSUser was not called");


            //returns null
            _uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Course)null).Verifiable("GetByIdAsync Course was not called");

            //create a passable function 
            Func<Task> action = (async () => await _controller.GetCourseClassesDataTable(_inputDTModel, courseId, lmsUserId, isTrainer, isLearner));


            await Assert.ThrowsAsync<NotFoundException>(action);



        }


        //User does not exist , throw not found
        [Theory]
        [InlineData(1, 1, true, false)]
        [InlineData(1, 1, false, true)]
        [InlineData(1, 1, true, true)]
        public async Task GetCourseClassesDataTableTest_UserDoesNotExist_ThrowNotFound(int? courseId, int? lmsUserId, bool isTrainer = false, bool isLearner = false)
        {

            //returns user based on the ID passed 
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((LMSUser)null).Verifiable("GetByIdAsync LMSUser was not called");


            //create a passable function 
            Func<Task> action = (async () => await _controller.GetCourseClassesDataTable(_inputDTModel, courseId, lmsUserId, isTrainer, isLearner));



            await Assert.ThrowsAsync<NotFoundException>(action);


            //verify that id of the current user is retreived 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()));



        }



        //[Fact()]
        //public void IsCourseClassModifiableTest()
        //{

        //    //start class is after current datetime
        //    var cc = TestCourseClassCreator();
        //    cc.StartClass = DateTime.Now.AddDays(1); 
        //    Assert.True(_controller.IsCourseClassModifiable(cc), "This test needs an implementation");


        //    //start class is before datetime
        //    cc.StartClass = DateTime.Now.AddDays(-1);
        //    Assert.False(_controller.IsCourseClassModifiable(cc), "This test needs an implementation");
        //}





    }
}