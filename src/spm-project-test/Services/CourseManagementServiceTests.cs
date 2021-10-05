using Xunit;
using SPM_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_Project.Repositories.Interfaces;
using Moq;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.Repositories;
using SPM_Project.Data;
using SPM_Project.DataTableModels.DataTableRequest;

namespace SPM_Project.Services.Tests
{
    //SHUM CHIN NING TESTS
    public class CourseManagementServiceTests:IDisposable
    {

         public CourseManagementService _service;

         public Mock<IUnitOfWork> _mockUnitOfWork;

         public Mock<ICourseRepository> _mockCourseRepository;



        //setup------------------------------------------------------------------

        //If the CLASS you are testing has any dependencies , MOCK those dependencies 
        //In this case we have to mock the unit of work & course repository 
        public CourseManagementServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCourseRepository = new Mock<ICourseRepository>();
            _service = new CourseManagementService(_mockUnitOfWork.Object);
        }

        private T DbContextOptions<T>()
        {
            throw new NotImplementedException();
        }

        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _mockUnitOfWork = null;
            _service = null; 
        }

        //[Fact()]
        //public async Task GetCourseClassesForTrainerDataTableTest()
        //{
        //    Assert.True(false, "This test needs an implementation");
        //}



        //if you are not validating your input or manipulate it ; just mock it 
        [Fact()]
        public async Task GetCourseDataTableTest()
        {
            var correctInput = new DTParameterModel()
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
                        Column=0,
                        Dir="asc"
                        } }
                    },

                Columns = new List<DTColumn>()
                    {
                        {
                            new DTColumn()
                            {
                                Data= "CourseName",
                                Name=  "CourseName",
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
                                Data= "NumberOfClasses",
                                Name= "NumberOfClasses",
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
                                Data= "CreatedDate",
                                Name= "CreatedDate",
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
                                Data= "UpdatedDate",
                                Name= "UpdatedDate",
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
                                Searchable= false,
                                Orderable= false,
                                Search = new DTSearch(){
                                    Value = "",
                                    Regex = false,
                                } ,
                            }
                        },
                    },
            };

            //moq sets up the values for the property 
            var input = new DTParameterModel();

            var output = new DTResponse<CourseTableData>();


            //return the object as indicated when i pass the input
            _mockUnitOfWork.Setup(l => l.CourseRepository).Returns(_mockCourseRepository.Object).Verifiable();
            _mockCourseRepository.Setup(l => l.GetCoursesDataTable(input)).ReturnsAsync(output).Verifiable();


            //call the function 
            var result = await _service.GetCoursesDataTable(input);

            //verify that the mocks are called 
            _mockUnitOfWork.Verify();
            _mockCourseRepository.Verify();

            //Do your asserts 
            Assert.NotNull(result);
            Assert.IsType<DTResponse<CourseTableData>>(result); 



            //create a passable function 
            //Func<Task> action = (async () => await _service.GetCoursesDataTable(input.Object)); 


            //pass if function is not implemented (COMMNENT THIS OUT AFTER THE FUNCTION IS IMPLEMENTED)
            //await Assert.ThrowsAsync<System.NotImplementedException>(action);





     
            //var result = await _service.GetCoursesForAdminDataTable(input.Object);

            //Assert.NotNull(result);
            //Assert.IsType<DTResponse<CourseTableData>>(result);


        }
   
    
    }
}