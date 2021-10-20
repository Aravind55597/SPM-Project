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

namespace SPM_Project.ApiControllers.Tests
{
    public class CourseClassesControllerTests
    {

        private CourseClassesController _controller;
        private UOWMocker _uowMocker;

        private DTParameterModel _inputDTModel;

        private DTResponse<CourseClassTableData> _outputDTModel;
        
        
        
        
        //private CourseClass TestCourseClassCreator()
        //{
        //    var courseClass = new CourseClass()
        //    {

        //        Name = "Test Course Class",
        //        StartRegistration = DateTime.Now,
        //        EndRegistration = DateTime.Now,
        //        StartClass = DateTime.Now,
        //        EndClass = DateTime.Now,
        //        ClassTrainer = new LMSUser()
        //        {

        //        },
        //        Course = new Course()
        //        {
        //            Name = "Test Course",
        //            Description = "Test Description",
        //            PassingPercentage = (decimal)0.85
        //        },


        //    }; 
        //} 
        
        
        
        
        
        
        public CourseClassesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new CourseClassesController(_uowMocker.mockUnitOfWork.Object);

            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<CourseClassTableData>();

            
            
            
            
            

            //mock when GetCouseClassesDataTable is called
            _uowMocker.mockCourseClassRepository.Setup(l => l.GetCourseClassesDataTable(_inputDTModel, It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(_outputDTModel);

            _uowMocker.mockCourseClassRepository.Setup(l=>l.GetAllAsync(c => c.Id == 1,default, "Course ClassTrainer" ,default,default)).ReturnsAsync(new List<CourseClass>());

           
            //"Course ClassTrainer"
            _uowMocker.mockCourseClassRepository.
                Setup(l => l.GetByIdAsync(1,"Course ClassTrainer")).
                ReturnsAsync(new CourseClass() { 
                    Name="Test Course Class",
                    StartRegistration=DateTime.Now,
                    EndRegistration = DateTime.Now,
                    StartClass = DateTime.Now,
                    EndClass = DateTime.Now,
                    ClassTrainer = new LMSUser()
                    {

                    }, 
                    Course = new Course()
                    {
                        Name="Test Course",
                        Description= "Test Description",
                        PassingPercentage = (decimal)0.85
                    },


                });

            //returns 1
            //_uowMocker.mockLMSUserRepository.Setup(l => l.RetrieveCurrentUserIdAsync()).ReturnsAsync(1);
            //_uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new LMSUser()); 
            ////course 1 exists , course 2 does not exists
            //_uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(1)).ReturnsAsync(new Course());
            //_uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(2)).ReturnsAsync(null);

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

            var result = await _controller.GetCourseClassesDTOAPIAsync(null, 1) as OkObjectResult;
            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<List<CourseClassesDTO>>(items);
            // Then
            Assert.IsType<List<CourseClassesDTO>>(deserializedMessage);
        }

        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_CourseDoesNotExists_ReturnsNotFoundException()
        {
            Func<Task> action = (async () => await _controller.GetCourseClassesDTOAPIAsync(null,0));
            //check if ok is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);
        }



        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_RetreiveOneClass_ReturnsOK()
        {

            var result = await _controller.GetCourseClassesDTOAPIAsync(1, null) as OkObjectResult;
            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<CourseClassesDTO>(items);
            // Then
            Assert.IsType<CourseClassesDTO>(deserializedMessage);
        }



        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassesDTOAPIAsync_RetreiveAllClasses_ReturnsOK()
        {

            var result = await _controller.GetCourseClassesDTOAPIAsync(null, null) as OkObjectResult;

            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<List<CourseClassesDTO>>(items);
            // Then
            Assert.IsType<List<CourseClassesDTO>>(deserializedMessage);
        }


        //GetCourseClassDTOAsync-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassDTOAsync_ClassExists_ReturnsOneClass()
        {
            //assert that 1 class is returned 
            var result = await _controller.GetCourseClassDTOAsync(1); 

            Assert.IsType<CourseClassesDTO>(result);
        }

        //BLACKBOX
        [Fact()]
        public async Task GetCourseClassDTOAsync_ClassDoesNotExists_ThrowNotFoundException()
        {
            Func<Task> action = (async () => await _controller.GetCourseClassDTOAsync(0));

            await Assert.ThrowsAsync<NotFoundException>(action);

        }


        










        ////BLACKBOX
        //[Fact()]
        //public async Task GetCourseClassesAsync_NoClassId_ReturnListOfClasses()
        //{


        //    //aasert that a list of classes is returned ; 

        //}




        //[NonAction]
        //public async Task<IActionResult> RetreiveCourseClasses(int? id)
        //{


        //    if (id != null)
        //    {
        //        //check if class exists ; otherwise return not found 
        //        //return courseclass
        //    }

        //    //return classes 

        //    throw new NotImplementedException();

        //}

























































        //GetCourseClassesDataTable-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

    }
}