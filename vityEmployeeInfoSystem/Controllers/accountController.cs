using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace vityEmployeeInfoSystem.Controllers
{
    public class accountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }

    }
}
