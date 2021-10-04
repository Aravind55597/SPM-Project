using Moq;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableRequest;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SPM_Project.Services.Tests
{
    public class UserManagementServiceTests: IDisposable
    {
        //input
        public DTParameterModel _engineersDataTableInput;

        //output
        public DTResponse<EngineersTableData> _engineersDataTableOutput;

        public Mock<IUnitOfWork> _mockUnitOfWork;

        public Mock<ILMSUserRepository> _mockLMSUserRepository;

        public UserManagementService _userManagementService;





        //SETUP-------------------------------------------------------------------
        public  UserManagementServiceTests()
        {

            _engineersDataTableInput = new DTParameterModel()
            {
                Draw = 1,
                Start = 0,
                Length = 5,
                Search = new DTSearch()
                {
                    Value = "",
                    Regex = false,
                },
                Order = new List<DTOrder>()
                    {
                        { new DTOrder(){
                        Column=1,
                        Dir="asc"
                        } }
                    },

                Columns = new List<DTColumn>()
                    {
                        {
                            new DTColumn()
                            {
                                Data= null,
                                Name= "Checkbox",
                                Searchable= true,
                                Orderable= false,
                                Search = new DTSearch(){
                                    Value = "",
                                    Regex = false,
                                } ,
                            }
                        },

                        {
                            new DTColumn()
                            {
                                Data= "Id",
                                Name= "Id",
                                Searchable= true,
                                Orderable= true,
                                Search = new DTSearch(){
                                    Value = "",
                                    Regex = false,
                                } ,
                            }
                        },

                        {
                            new DTColumn()
                            {
                                Data= "Name",
                                Name= "Name",
                                Searchable= true,
                                Orderable= true,
                                Search = new DTSearch(){
                                    Value = "",
                                    Regex = false,
                                } ,
                            }
                        },

                        {
                            new DTColumn()
                            {
                                Data= "Role",
                                Name= "Role",
                                Searchable= true,
                                Orderable= true,
                                Search = new DTSearch(){
                                    Value = "",
                                    Regex = false,
                                } ,
                            }
                        },
                         {
                            new DTColumn()
                            {
                                Data= "Actions",
                                Name= "Actions",
                                Searchable= true,
                                Orderable= false,
                                Search = new DTSearch(){
                                    Value = "",
                                    Regex = false,
                                } ,
                            }
                        },
                    },
            };

            _engineersDataTableOutput = new DTResponse<EngineersTableData>();


             _mockLMSUserRepository = new Mock<ILMSUserRepository>();
            _mockLMSUserRepository.Setup(l => l.GetEngineersDataTable(_engineersDataTableInput) ).ReturnsAsync(_engineersDataTableOutput).Verifiable();


            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.LMSUserRepository).Returns(_mockLMSUserRepository.Object); 


            //create the service object & pass the mock unitofwork 
            _userManagementService = new UserManagementService(_mockUnitOfWork.Object);

        }


        //TEARDOWN-------------------------------------------------------------------
        public void Dispose()
        {
            _engineersDataTableInput = null;
            _mockLMSUserRepository = null;
            _mockUnitOfWork = null;
            _userManagementService = null;
        }





        [Fact]
        public async Task GetEngineersDataTableTest()
        {

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            var actual = await _userManagementService.GetEngineersDataTable(_engineersDataTableInput);

            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------

            //verify that GetEngineersDataTable was called!!!!!!!!!!!!!!!
            _mockLMSUserRepository.Verify(); 

            //verify that you did not get null as a result 
            Assert.NotNull(actual);
            Assert.IsType<DTResponse<EngineersTableData>>(actual); 

        }




    }
}