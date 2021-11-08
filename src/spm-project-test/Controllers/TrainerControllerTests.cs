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
    public class TrainerControllerTests : IDisposable
    {

        public TrainerController _controller;

        //setup--------------------------------------------------
        public TrainerControllerTests()
        {
            _controller = new TrainerController();
        }

        //setup--------------------------------------------------


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _controller = null;
        }


        [Fact()]
        public void CreateQuizTest_Check_If_Correct_Page_Is_Returned()
        {
            var controller = new TrainerController().WithIdentity("Trainer");

            var result = controller.CreateQuiz() as ViewResult;

            var checkAttribute = controller.GetType().GetMethod("CreateQuiz").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.NotNull(result);
            Assert.Equal(typeof(AuthorizeAttribute), checkAttribute[0].GetType());
            Assert.Equal("CreateQuiz", result.ViewName);
        }

        [Fact()]
        public void CreateQuizTest_Check_If_Non_Trainer_Users_Can_Access()
        {
            var attribute = _controller.GetAuthoriseAttribute("CreateQuiz");
            Assert.NotNull(attribute);

            var checkAdmin = attribute.CheckRoleAccess("Administrator");
            Assert.False(checkAdmin);

            var checkLearner = attribute.CheckRoleAccess("Learner");
            Assert.False(checkLearner);
        }


    }
}







