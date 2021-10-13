using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;

namespace SPM_Project.ApiControllers.Tests
{
    public class ClassEnrollmentRecordControllerTests:IDisposable
    {

        private ClassEnrollmentRecordController _controller;
        private UOWMocker _uowMocker;


        public  ClassEnrollmentRecordControllerTests()
        {
          //  Assert.True(false, "This test needs an implementation");

            _uowMocker = new UOWMocker();
            _controller = new ClassEnrollmentRecordController(_uowMocker.mockUnitOfWork.Object);
            //_inputDTModel = new DTParameterModel();
            //_outputDTModel = new DTResponse<CourseClassTableData>();

            _uowMocker.mockUnitOfWork.Setup(u => u.CompleteAsync()).Verifiable("CompleteAsync is NOT called"); 

            _uowMocker.mockUnitOfWork.Setup(l => l.CourseRepository).Returns(_uowMocker.mockCourseRepository.Object).Verifiable("Mock Course Repository is NOT returned");
            _uowMocker.mockUnitOfWork.Setup(l => l.CourseClassRepository).Returns(_uowMocker.mockCourseClassRepository.Object).Verifiable("Mock CourseClass Repository is NOT returned");
            _uowMocker.mockUnitOfWork.Setup(l => l.LMSUserRepository).Returns(_uowMocker.mockLMSUserRepository.Object).Verifiable("Mock LMSUser Repository is NOT returned");



        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }

        [Fact()]
        public async Task AddEnrollmentRecordTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddEnrollmentRecordTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}