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
            return View();
        }
    }
}
