using Xunit;
using SPM_Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SPM_Project.Controllers.Tests
{

    public class LearnerControllerTests : IDisposable
    {
        public LearnerController _controller;

        //setup--------------------------------------------------
        public LearnerControllerTests()
        {
            _controller = new LearnerController();
        }

        //setup--------------------------------------------------


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _controller = null;
        }

        //Pung Xian Wei
        [Fact()]
        public void ViewCoursesTestWhenLearner()
        {
            var controller = new LearnerController().WithIdentity("Learner");

            var result = controller.ViewCourses() as ViewResult;

            var checkAttribute = controller.GetType().GetMethod("ViewCourses").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.NotNull(result);
            Assert.Equal(typeof(AuthorizeAttribute), checkAttribute[0].GetType());
            Assert.Equal("ViewCourses", result.ViewName);
        }

        [Fact()]
        public void ViewCoursesTest_Check_If_Non_Learner_Users_Can_Access()
        {
            var attribute = _controller.GetAuthoriseAttribute("ViewCourses");
            Assert.NotNull(attribute);

            var checkTrainer = attribute.CheckRoleAccess("Trainer");
            Assert.False(checkTrainer);

            var checkAdministrator = attribute.CheckRoleAccess("Administrator");
            Assert.False(checkAdministrator);

        }

        [Fact()]
        public void ViewRequestsTest_Check_If_Correct_Page_Is_Returned()
        {
            var controller = new LearnerController().WithIdentity("Learner");

            var result = controller.ViewRequests() as ViewResult;

            var checkAttribute = controller.GetType().GetMethod("ViewRequests").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.NotNull(result);
            Assert.Equal(typeof(AuthorizeAttribute), checkAttribute[0].GetType());
            Assert.Equal("ViewRequests", result.ViewName);
        }

        [Fact()]
        public void ViewRequestsTest_Check_If_Non_Learner_Users_Can_Access()
        {
            var attribute = _controller.GetAuthoriseAttribute("ViewRequests");
            Assert.NotNull(attribute);

            var checkTrainer = attribute.CheckRoleAccess("Trainer");
            Assert.False(checkTrainer);

            var checkAdmin = attribute.CheckRoleAccess("Administrator");
            Assert.False(checkAdmin);
        }
    }
}