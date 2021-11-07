using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SPM_Project.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Trainer")]
        public IActionResult CreateQuiz()
        {
            return View("CreateQuiz");
        }

        [Authorize(Roles = "Trainer")]
        public IActionResult SubmitQuiz([FromQuery] int? chapterid, [FromQuery] int? courseClassId, [FromQuery] int? quizId)
        {

            ViewBag.chapterid = chapterid;

            ViewBag.courseClassId = courseClassId;

            ViewBag.quizId = quizId;


            return View("SubmitQuiz");
        }
    }
}
