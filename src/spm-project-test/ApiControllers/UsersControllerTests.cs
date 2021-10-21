using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.Repositories.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SPM_ProjectTests.Mocks;
using SPM_Project.EntityModels;
using SPM_Project.CustomExceptions;
using Newtonsoft.Json;

namespace SPM_Project.ApiControllers.Tests
{
    public class UsersControllerTests : IDisposable
    {

        private DTParameterModel _inputDTModel;

        private DTResponse<LMSUsersTableData> _outputDTModel;

        private UOWMocker _uowMocker;

        public UsersController _controller;

        //function namming follows 
        //https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
        public UsersControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new UsersController(_uowMocker.mockUnitOfWork.Object);
            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<LMSUsersTableData>();
            //the input for this function does not matter for testing as the datatable function jsut calls this to return the result ; the valiadation of the input to this 
            //class is handled by the datatabale function
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetEngineersDataTable(_inputDTModel, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int?>())).ReturnsAsync(_outputDTModel).Verifiable("GetEngineersDataTable was not called");
        }



        //TEARDOWN-------------------------------------------------------------------
        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }




        //GetEngineersDataTable-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Theory]
        [InlineData(1, false, true, true)]
        [InlineData(1, true, false, true)]
        [InlineData(1, false, false, false)]
        public async Task GetEngineersDataTableTest_ClassDoesNotExist_ThrowNotFoundException(int? classId, bool isTrainer = false, bool isLearner = false, bool isEligible = false)
        {

            //setup
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((CourseClass)null).Verifiable("Retreiving courseClass  was not attempted"); 


            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, classId, isTrainer, isLearner, isEligible));


            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------

            await Assert.ThrowsAsync<NotFoundException>(action);

        }



        [Theory]
        [InlineData(1, false, true, true)]
        [InlineData(1, true, false, true)]
        [InlineData(1, false, false, false)]
        public async Task GetEngineersDataTableTest_ClassExists_ReturnOK(int? classId, bool isTrainer = false, bool isLearner = false, bool isEligible = false)
        {
            //setup 
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new CourseClass()).Verifiable("Retreiving course was not attempted");

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            var result = await _controller.GetEngineersDataTable(_inputDTModel, classId, isTrainer, isLearner, isEligible) as OkObjectResult;

            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------


            //verify that class retreival was attempted 
            _uowMocker.mockCourseClassRepository.Verify(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()));

            //verify that respository is retreived 
            _uowMocker.mockUnitOfWork.Verify(l => l.LMSUserRepository);

            //verify that repository functionw as called 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetEngineersDataTable(_inputDTModel, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int?>()));

            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<DTResponse<LMSUsersTableData>>(items);
            // Then
            Assert.IsType<DTResponse<LMSUsersTableData>>(deserializedMessage);
        }



        [Theory]
        [InlineData(null, true, false, true)]
        [InlineData(null, true, true, true)]
        [InlineData(null, false, false, true)]
        [InlineData(null, false, true, true)]
        [InlineData(1, true, true, true)]
        [InlineData(1, false, false, true)]
        public async Task GetEngineersDataTableTest_IsEligibleProvidedWithWrongRequest_ThrowBadRequest(int? classId, bool isTrainer = false, bool isLearner = false, bool isEligible = false)
        {


            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, classId, isTrainer, isLearner, isEligible));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------


            await Assert.ThrowsAsync<BadRequestException>(action);

        }






        [Theory]
        [InlineData(null, false, true, false)]
        [InlineData(null, true, false, false)]
        [InlineData(null, true, true, false)]
        public async Task GetEngineersDataTableTest_IsEligbibleNotProvidedWithWrongRequest__ThrowBadRequest(int? classId, bool isTrainer = false, bool isLearner = false, bool isEligible = false)
        {

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            // true trainer , false learner
            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, classId, isTrainer, isLearner, isEligible));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);


        }





    }
}




