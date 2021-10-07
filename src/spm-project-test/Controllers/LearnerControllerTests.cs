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
    public class LearnerControllerTests
    {
        //Xian Wei
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
    }
}