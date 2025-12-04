using Microsoft.AspNetCore.Mvc;

namespace StudentProjectPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
    }
}
