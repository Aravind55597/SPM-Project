using Microsoft.AspNetCore.Mvc;
namespace SPM_Project.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Courses_teaching()
        {
            return View();
        }
    }
}
