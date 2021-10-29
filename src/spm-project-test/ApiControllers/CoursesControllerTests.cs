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

    //TODO ASSERT THE EXCEPTION MESSAGE 
    //https://www.meziantou.net/quick-introduction-to-xunitdotnet.htm
    public class CoursesControllerTests:IDisposable
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
       

        //TODO FIX THIS TEST
        [Fact()]
        public async Task GetCourseDataTableTest_FunctionReturnsObjectReturnedByRepository_Returns_DTResponse()
        {
           

            var output = new DTResponse<CourseTableData>();


            //return the object as indicated when i pass the input
            _uowMocker.mockUnitOfWork.Setup(l => l.CourseRepository).Returns(_uowMocker.mockCourseRepository.Object).Verifiable();
            _uowMocker.mockCourseRepository.Setup(l => l.GetCoursesDataTable(_inputDTModel)).ReturnsAsync(_outputDTModel).Verifiable();


            //call the function 
            var result = await _controller.GetCoursesDataTable(_inputDTModel);

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