using Xunit;
using SPM_Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SPM_ProjectTests.Extensions;

namespace SPM_Project.Controllers.Tests
{
  
    public class AdministratorControllerTests:IDisposable
    {

        public AdministratorController _controller; 

        //setup--------------------------------------------------
        public AdministratorControllerTests()
        {
            _controller =  new AdministratorController();
        }

        //setup--------------------------------------------------


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _controller = null; 
        }


        //ViewEngineers-------------------------------------------------------------------

        //SHUM CHIN NING 
        [Fact()]
        public void ViewEngineersTest_Check_If_Correct_Page_Is_Returned()
        {
           
            var result = _controller.ViewEngineers() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("ViewEngineers", result.ViewName);

        }

        [Fact()]
        public void ViewEngineersTest_Check_If_Non_Admin_Users_Can_Access()
        {
            var attribute = _controller.GetAuthoriseAttribute("ViewEngineers");
            Assert.NotNull(attribute);

            var checkTrainer = attribute.CheckRoleAccess("Trainer");
            Assert.False(checkTrainer);

            var checkLearner = attribute.CheckRoleAccess("Learner");
            Assert.False(checkLearner);

        }

        //ViewAllCourses-------------------------------------------------------------------


        //SHUM CHIN NING
        [Fact()]
        public void ViewAllCoursesTest_Check_If_Correct_Page_Is_Returned()
        {
            var result = _controller.ViewAllCourses() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("ViewAllCourses", result.ViewName);
        }



        [Fact()]
        public void ViewAllCoursesTest_Check_If_Non_Admin_Users_Can_Access()
        {
            var attribute = _controller.GetAuthoriseAttribute("ViewAllCourses");
            Assert.NotNull(attribute);

            var checkTrainer = attribute.CheckRoleAccess("Trainer");
            Assert.False(checkTrainer);

            var checkLearner = attribute.CheckRoleAccess("Learner");
            Assert.False(checkLearner);
        }


    }
}