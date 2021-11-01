using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Controllers
{
    public class LearnerController : Controller
    {
        [Authorize(Roles = "Learner")]
        public IActionResult ViewCourses()
        {
            return View("ViewCourses");
        }

        [Authorize(Roles = "Learner")]
        public IActionResult ViewChapters()
        {
            return View("ViewChapters");
        }

        [Authorize(Roles = "Learner")]
        public IActionResult ViewRequests()
        {
            return View("ViewRequests");
        }
    }
}
