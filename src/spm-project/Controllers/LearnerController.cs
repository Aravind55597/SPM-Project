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
        public IActionResult ViewEnrolledCourses()
        {
            return View("ViewEnrolledCourses");
        }

        [Authorize(Roles = "Learner")]
        [HttpGet]
        public IActionResult ViewCourseMaterial([FromQuery] int chapterId, [FromQuery] int courseClassId, [FromQuery] int gradedQuizId)
        {
            ViewBag.ChapterId = chapterId;
            ViewBag.CourseClassId = courseClassId;
            ViewBag.GradedQuizId= gradedQuizId;


            return View("ViewCourseMaterial");
        }

        [Authorize(Roles = "Learner")]
        public IActionResult ViewRequests()
        {
            return View("ViewRequests");
        }
    }
}
