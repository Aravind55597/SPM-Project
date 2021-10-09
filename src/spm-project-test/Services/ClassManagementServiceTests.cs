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

namespace SPM_Project.Services.Tests
{
    public class ClassManagementServiceTests
    {

        //private ClassManagementService _service;

        //private UOWMocker _uowMocker;


        ////setup
        //public ClassManagementServiceTests()
        //{
        //    _uowMocker = new UOWMocker();
        //    _service = new ClassManagementService(_uowMocker.mockUnitOfWork.Object);
        //    //returns 1
        //    _uowMocker.mockLMSUserRepository.Setup(l => l.RetrieveCurrentUserIdAsync()).ReturnsAsync(1);

        //    //course 1 exists , course 2 does not exists
        //    _uowMocker.mockCourseRepository.Setup(l=>l.GetByIdAsync(2)).ReturnsAsync(); 
        //}

        ////teardown
        //public void Dispose()
        //{
        //    _uowMocker = null;
        //    _service = null;
        //}


        //[Fact()]
        //public async Task GetCourseClassesDataTableTest_()
        //{
            
        //}







    }
}