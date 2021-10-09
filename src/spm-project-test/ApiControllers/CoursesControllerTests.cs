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
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace SPM_Project.ApiControllers.Tests
{
    public class CoursesControllerTests
    {
        private UOWMocker _uowMocker;

        private CoursesController _controller;

        private DTParameterModel _inputDTModel;

        private DTResponse<CourseTableData> _outputDTModel;

        //setup------------------------------------------------------------------

        //If the CLASS you are testing has any dependencies , MOCK those dependencies 
        //In this case we have to mock the unit of work & course repository 
        public CoursesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new CoursesController(_uowMocker.mockUnitOfWork.Object);
        }


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }
        //if you are not validating your input or manipulate it ; just mock it 
        [Fact()]
        public async Task GetCourseDataTableTest_FunctionReturnsObjectReturnedByRepository_Returns_DTResponse()
        {
            //var correctInput = new DTParameterModel()
            //{
            //    Draw = 1,
            //    Start = 0,
            //    Length = 5,
            //    Search = new DTSearch()
            //    {
            //        Value = "",
            //        Regex = false,
            //    },
            //    Order = new List<DTOrder>()
            //        {
            //            { new DTOrder(){
            //            Column=0,
            //            Dir="asc"
            //            } }
            //        },

            //    Columns = new List<DTColumn>()
            //        {
            //            {
            //                new DTColumn()
            //                {
            //                    Data= "CourseName",
            //                    Name=  "CourseName",
            //                    Searchable= true,
            //                    Orderable= true,
            //                    Search = new DTSearch(){
            //                        Value = "",
            //                        Regex = false,
            //                    } ,
            //                }
            //            },

            //            {
            //                new DTColumn()
            //                {
            //                    Data= "NumberOfClasses",
            //                    Name= "NumberOfClasses",
            //                    Searchable= true,
            //                    Orderable= true,
            //                    Search = new DTSearch(){
            //                        Value = "",
            //                        Regex = false,
            //                    } ,
            //                }
            //            },

            //            {
            //                new DTColumn()
            //                {
            //                    Data= "CreatedDate",
            //                    Name= "CreatedDate",
            //                    Searchable= true,
            //                    Orderable= true,
            //                    Search = new DTSearch(){
            //                        Value = "",
            //                        Regex = false,
            //                    } ,
            //                }
            //            },

            //            {
            //                new DTColumn()
            //                {
            //                    Data= "UpdatedDate",
            //                    Name= "UpdatedDate",
            //                    Searchable= true,
            //                    Orderable= true,
            //                    Search = new DTSearch(){
            //                        Value = "",
            //                        Regex = false,
            //                    } ,
            //                }
            //            },
            //             {
            //                new DTColumn()
            //                {
            //                    Data= "Actions",
            //                    Name= "Actions",
            //                    Searchable= false,
            //                    Orderable= false,
            //                    Search = new DTSearch(){
            //                        Value = "",
            //                        Regex = false,
            //                    } ,
            //                }
            //            },
            //        },
            //};

            //moq sets up the values for the property 
          

            var output = new DTResponse<CourseTableData>();


            //return the object as indicated when i pass the input
            _uowMocker.mockUnitOfWork.Setup(l => l.CourseRepository).Returns(_uowMocker.mockCourseRepository.Object).Verifiable();
            _uowMocker.mockCourseRepository.Setup(l => l.GetCoursesDataTable(_inputDTModel)).ReturnsAsync(_outputDTModel).Verifiable();


            //call the function 
            var result = await _controller.GetCoursesDataTable(_inputDTModel);

            //verify that the mocks are called 
            _uowMocker.mockUnitOfWork.Verify();
            _uowMocker.mockCourseRepository.Verify();

            //Do your asserts 
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult>(result);



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