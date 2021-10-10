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

namespace SPM_Project.ApiControllers.Tests
{
    public class UsersControllerTests:IDisposable
    {

        private DTParameterModel _inputDTModel;

        private DTResponse<LMSUsersTableData> _outputDTModel;

        private UOWMocker _uowMocker;

        public UsersController _controller;


        public UsersControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new UsersController(_uowMocker.mockUnitOfWork.Object);
            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<LMSUsersTableData>();
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetEngineersDataTable(_inputDTModel, true, true , It.IsAny<int?>())).ReturnsAsync(_outputDTModel);
        }



        //TEARDOWN-------------------------------------------------------------------
        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }




        //GetCourseClassesDataTable-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Fact]
        public async Task GetEngineersDataTableTest_ClassDoesNotExist_ThrowNotFound()
        {

            //setup

            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((CourseClass)null);


            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetEngineersDataTable(_inputDTModel, 1, true, true));


            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------

            await Assert.ThrowsAsync<NotFoundException>(action);


        }



        [Fact]
        public async Task GetEngineersDataTableTest_ClassExists_ReturnOK()
        {
            //setup 


            _uowMocker.mockCourseClassRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new CourseClass());

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            var result = await _controller.GetEngineersDataTable(_inputDTModel, 1,true ,true ) as OkObjectResult;

            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------


            //verify that you did not get null as a result 
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<DTResponse<LMSUsersTableData>>(result.Value);

        }





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