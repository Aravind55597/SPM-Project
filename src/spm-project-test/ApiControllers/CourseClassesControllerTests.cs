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

namespace SPM_Project.ApiControllers.Tests
{
    public class CourseClassesControllerTests
    {

        private CourseClassesController _controller;
        private UOWMocker _uowMocker;

        private DTParameterModel _inputDTModel;

        private DTResponse<CourseClassTableData> _outputDTModel;
        public CourseClassesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new CourseClassesController(_uowMocker.mockUnitOfWork.Object);
            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<CourseClassTableData>();

            //mock when GetCouseClassesDataTable is called
            _uowMocker.mockCourseClassRepository.Setup(l => l.GetCourseClassesDataTable(_inputDTModel, It.IsAny<int?>(), It.IsAny<int>(), true, true)).ReturnsAsync(_outputDTModel);

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




        //GetCourseClassesDataTable-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //No User ID is passed 
        [Fact()]
        public async Task GetCourseClassesDataTableTest_NoLMSUserIdIsGiven_ReturnTableData()
        {

            //returns id of current user 
            _uowMocker.mockLMSUserRepository.Setup(l => l.RetrieveCurrentUserIdAsync()).ReturnsAsync(1).Verifiable("Id of the current user was NOT retreived");

            //returns a course (empty object)
            _uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new Course()).Verifiable("Course is NOT retrieved");

            var result = await _controller.GetCourseClassesDataTable(_inputDTModel, 1, null, true, true);

            //verify that id of the current user is retreived 
            _uowMocker.mockLMSUserRepository.Verify(l => l.RetrieveCurrentUserIdAsync());
            //verify that course is retreived 
            _uowMocker.mockCourseRepository.Verify(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>()));
            //verify that function returns a DTResposne form repostiory 
            Assert.IsAssignableFrom<ActionResult>(result);

        }

        //Course does not exist , function throws not found
        [Fact()]
        public async Task GetCourseClassesDataTableTest_CourseDoesNotExist_ThrowNotFound()
        {

            //returns user based on the ID passed 
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new LMSUser()).Verifiable("GetByIdAsync LMSUser was not called");


            //returns null
            _uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync((Course)null).Verifiable("GetByIdAsync Course was not called");

            //create a passable function 
            Func<Task> action = (async () => await _controller.GetCourseClassesDataTable(_inputDTModel, 1, 1, true, true));


            await Assert.ThrowsAsync<NotFoundException>(action);


            //verify that id of the current user is retreived 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>()));

            //verify that all mockCourseRepository setup is called
            _uowMocker.mockCourseRepository.Verify(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>()));


        }


        //User does not exist , throw not found
        [Fact()]
        public async Task GetCourseClassesDataTableTest_UserDoesNotExist_ThrowNotFound()
        {

            //returns user based on the ID passed 
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync((LMSUser)null).Verifiable("GetByIdAsync LMSUser was not called");


            //returns empty course
            //_uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new Course()).Verifiable("GetByIdAsync Course was not called");

            //create a passable function 
            Func<Task> action = (async () => await _controller.GetCourseClassesDataTable(_inputDTModel, 1, 1, true, true));


   
            await Assert.ThrowsAsync<NotFoundException>(action);


            //verify that id of the current user is retreived 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>()));



        }

    }
}