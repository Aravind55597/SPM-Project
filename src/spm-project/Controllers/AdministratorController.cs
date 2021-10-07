using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Controllers
{

    public class AdministratorController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult ViewAllEngineers()
        {
            return View("ViewAllEngineers");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult ViewAllCourses()
        {
            return View("ViewAllCourses");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ViewAllClasses()
        {
            return View("ViewAllClasses");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult ViewAllRequests()
        {
            return View("ViewAllRequests");
        }



    }
}
