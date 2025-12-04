using Microsoft.AspNetCore.Mvc;

namespace StudentProjectPortal.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
