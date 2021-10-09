using Xunit;
using SPM_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_Project.Repositories.Interfaces;
using Moq;
using SPM_ProjectTests.Mocks;
using SPM_Project.EntityModels;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.DataTableModels.DataTableData;

namespace SPM_Project.Services.Tests
{
    public class ClassManagementServiceTests:IDisposable
    {

        private ClassManagementService _service;

        private UOWMocker _uowMocker;

        private DTParameterModel _inputDTModel;

        private DTResponse<CourseClassTableData> _outputDTModel;

        //setup
        public ClassManagementServiceTests()
        {
            _uowMocker = new UOWMocker();
            _service = new ClassManagementService(_uowMocker.mockUnitOfWork.Object);
            _inputDTModel= new DTParameterModel();
            _outputDTModel = new DTResponse<CourseClassTableData>();


            _uowMocker.mockUnitOfWork.Setup(l => l.CourseRepository).Returns(_uowMocker.mockCourseRepository.Object).Verifiable("Mock Course Repository is returned");
            _uowMocker.mockUnitOfWork.Setup(l => l.CourseClassRepository).Returns(_uowMocker.mockCourseClassRepository.Object).Verifiable("Mock CourseClass Repository is returned");
            _uowMocker.mockUnitOfWork.Setup(l => l.LMSUserRepository).Returns(_uowMocker.mockLMSUserRepository.Object).Verifiable("Mock LMSUser Repository is returned");

            //mock when GetCouseClassesDataTable is called
            _uowMocker.mockCourseClassRepository.Setup(l => l.GetCourseClassesDataTable(_inputDTModel, It.IsAny<int?>(), It.IsAny<int>(), true, true)).ReturnsAsync(_outputDTModel); 

            //returns 1
            //_uowMocker.mockLMSUserRepository.Setup(l => l.RetrieveCurrentUserIdAsync()).ReturnsAsync(1);
            //_uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new LMSUser()); 
            ////course 1 exists , course 2 does not exists
            //_uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(1)).ReturnsAsync(new Course());
            //_uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(2)).ReturnsAsync(null);

        }

        //teardown
        public void Dispose()
        {
            _uowMocker = null;
            _service = null;
        }


        [Fact()]
        public async Task GetCourseClassesDataTableTest_No_LMSUserId_Is_Passed()
        {

                //returns id of current user 
                _uowMocker.mockLMSUserRepository.Setup(l => l.RetrieveCurrentUserIdAsync()).ReturnsAsync(1).Verifiable("Id of the current user was retreived");

                //returns a course (empty object)
                _uowMocker.mockCourseRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Course()).Verifiable("Course is retrieved");

                var result = await _service.GetCourseClassesDataTable(_inputDTModel, 1, null, true, true);


                //verify that unit of wrok setup went through 
                _uowMocker.mockUnitOfWork.VerifyAll();
                //verify that id of the current user is retreived 
                _uowMocker.mockLMSUserRepository.VerifyAll();
                //verify that course is retreived 
                _uowMocker.mockCourseRepository.VerifyAll();
                //verify that function returns a DTResposne form repostiory 
                Assert.IsType<DTResponse<CourseClassTableData>>(result);
            
            //catch (Exception ex)
            //{
           
            //    Assert.False(true,$"{ex.Message}"); 
            //}

        }







    }
}