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
        //[Fact()]
        //public void HRTest()
        //{
        //    var controller = new AdministratorController().WithIdentity("Administrator");

        //    var result = controller.HR() as ViewResult;

        //    var checkAttribute = controller.GetType().GetMethod("HR").GetCustomAttributes(typeof(AuthorizeAttribute), true);

        //    Assert.Equal(typeof(AuthorizeAttribute),checkAttribute[0].GetType()); 
        //    Assert.NotNull(result); 
        //    Assert.Equal("HR",result.ViewName);
          
        //}
    }
}