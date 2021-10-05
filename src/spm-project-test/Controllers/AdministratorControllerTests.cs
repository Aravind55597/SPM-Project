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
  
    public class AdministratorControllerTests
    {

        //SHUM CHIN NING 
        [Fact()]
        public void ViewEngineersTestWhenAdmin()
        {
            var controller = new AdministratorController().WithIdentity("Administrator");

            var result = controller.ViewEngineers() as ViewResult;

            var checkAttribute = controller.GetType().GetMethod("ViewEngineers").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.NotNull(result);
            Assert.Equal(typeof(AuthorizeAttribute), checkAttribute[0].GetType());
            Assert.Equal("ViewEngineers", result.ViewName);

        }


        //SHUM CHIN NING
        [Fact()]
        public void ViewAllCoursesTestWhenAdmin()
        {
            var controller = new AdministratorController().WithIdentity("Administrator");

            var result = controller.ViewAllCourses() as ViewResult;

            var checkAttribute = controller.GetType().GetMethod("ViewAllCourses").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.NotNull(result);
            Assert.Equal(typeof(AuthorizeAttribute), checkAttribute[0].GetType());
            Assert.Equal("ViewAllCourses", result.ViewName);
        }






    }
}