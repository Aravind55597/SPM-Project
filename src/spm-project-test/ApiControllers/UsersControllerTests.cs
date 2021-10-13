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
    public class UsersControllerTests:IDisposable
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




        //GetCourseClassesDataTable-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------







        [Fact]
        public async Task GetEngineersDataTableTest_ClassBelongingToClassIdDoesNotExist()
        {

            //setup
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync((CourseClass)null).Verifiable("Retreiving course was not attempted"); ;


            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, 1));


            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------

            await Assert.ThrowsAsync<NotFoundException>(action);

        }

        [Fact]
        public async Task GetEngineersDataTableTest_ClassBelongingToClassIdDoesExists()
        {
            //setup 
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new CourseClass()).Verifiable("Retreiving course was not attempted");

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            var result = await _controller.GetEngineersDataTable(_inputDTModel, 1) as OkObjectResult;

            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------


            //verify that course retreival was attempted 
            _uowMocker.mockCourseClassRepository.Verify(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()));

            //verify that respository is retreived 
            _uowMocker.mockUnitOfWork.Verify(l => l.LMSUserRepository); 

            //verify that repository functionw as called 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetEngineersDataTable(_inputDTModel, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int?>()));
            //verify that you did not get null as a result 
            Assert.NotNull(result);
            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<DTResponse<LMSUsersTableData>>(items);
            // Then
            Assert.IsType<DTResponse<LMSUsersTableData>>(deserializedMessage);
        }

        [Fact]
        public async Task GetEngineersDataTableTest_IsEligibleProvided()
        {
            //setup 
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new CourseClass()).Verifiable("Retreiving course was not attempted"); ;

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            var result = await _controller.GetEngineersDataTable(_inputDTModel, 1) as OkObjectResult;

            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------

            //verify that course retreival was attempted 
            _uowMocker.mockCourseClassRepository.Verify(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()));

            //verify that respository is retreived 
            _uowMocker.mockUnitOfWork.Verify(l => l.LMSUserRepository);

            //verify that repository functionw as called 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetEngineersDataTable(_inputDTModel, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int?>()));
            //verify that you did not get null as a result 
            Assert.NotNull(result);
            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<DTResponse<LMSUsersTableData>>(items);
            // Then
            Assert.IsType<DTResponse<LMSUsersTableData>>(deserializedMessage);

        }


        [Fact]
        public async Task GetEngineersDataTableTest_IsEligbibleNotProvided()
        {
            //setup 
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new CourseClass()).Verifiable("Retreiving course was not attempted");
            
            
            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            // true trainer , false learner
            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, 1 , true, false));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);



            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            // false trainer , false learner
            var result = await _controller.GetEngineersDataTable(_inputDTModel, 1) as OkObjectResult;
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            //verify that course retreival was attempted 
            _uowMocker.mockCourseClassRepository.Verify(u => u.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()));

            //verify that respository is retreived 
            _uowMocker.mockUnitOfWork.Verify(l => l.LMSUserRepository);

            //verify that repository functionw as called 
            _uowMocker.mockLMSUserRepository.Verify(l => l.GetEngineersDataTable(_inputDTModel, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int?>()));
            //verify that you did not get null as a result 
            Assert.NotNull(result);
            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<DTResponse<LMSUsersTableData>>(items);
            // Then
            Assert.IsType<DTResponse<LMSUsersTableData>>(deserializedMessage);


            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            //false for trainer , true for learner 
            action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, 1, false, true));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);

            
            
            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            //true for trainer , true for learner 
            action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, 1, true, true));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);


        }
        [Fact]
        public async Task GetEngineersDataTableTest_IsEligbibleProvided()
        {
            //setup 
            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>() , It.IsAny<string>())).ReturnsAsync(new CourseClass());

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            // true for trainer , learner , eligible
            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, null, true, true, true));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            // false for trainer,learner, true for eligible
            action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, null, false, false, true));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------
            //false for trainer , learner , treu for eligible
            action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, null, false, false, true));
            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------
            await Assert.ThrowsAsync<BadRequestException>(action);


        }


        //sample input from front end 
        //        _engineersDataTableInput = new DTParameterModel()
        //        {
        //            Draw = 1,
        //            Start = 0,
        //            Length = 5,
        //            Search = new DTSearch()
        //            {
        //                Value = "",
        //                Regex = false,
        //            },
        //            Order = new List<DTOrder>()
        //                {
        //                    { new DTOrder(){
        //                    Column=1,
        //                    Dir="asc"
        //                    } }
        //                },

        //            Columns = new List<DTColumn>()
        //                {
        //                    {
        //                        new DTColumn()
        //                        {
        //                            Data= null,
        //                            Name= "Checkbox",
        //                            Searchable= true,
        //                            Orderable= false,
        //                            Search = new DTSearch(){
        //                                Value = "",
        //                                Regex = false,
        //                            } ,
        //                        }
        //                    },

        //                    {
        //                        new DTColumn()
        //                        {
        //                            Data= "Id",
        //                            Name= "Id",
        //                            Searchable= true,
        //                            Orderable= true,
        //                            Search = new DTSearch(){
        //                                Value = "",
        //                                Regex = false,
        //                            } ,
        //                        }
        //                    },

        //                    {
        //                        new DTColumn()
        //                        {
        //                            Data= "Name",
        //                            Name= "Name",
        //                            Searchable= true,
        //                            Orderable= true,
        //                            Search = new DTSearch(){
        //                                Value = "",
        //                                Regex = false,
        //                            } ,
        //                        }
        //                    },

        //                    {
        //                        new DTColumn()
        //                        {
        //                            Data= "Role",
        //                            Name= "Role",
        //                            Searchable= true,
        //                            Orderable= true,
        //                            Search = new DTSearch(){
        //                                Value = "",
        //                                Regex = false,
        //                            } ,
        //                        }
        //                    },
        //                     {
        //                        new DTColumn()
        //                        {
        //                            Data= "Actions",
        //                            Name= "Actions",
        //                            Searchable= true,
        //                            Orderable= false,
        //                            Search = new DTSearch(){
        //                                Value = "",
        //                                Regex = false,
        //                            } ,
        //                        }
        //                    },
        //                },
        //        };


    }
}