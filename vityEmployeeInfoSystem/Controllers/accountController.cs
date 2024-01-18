using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using vityEmployeeInfoSystem.Models;

namespace vityEmployeeInfoSystem.Controllers
{
    public class accountController : Controller
    {
        private readonly AgroEmployeeDbContext _context; 
        public accountController(AgroEmployeeDbContext context)
        {
            _context = context;
        }

  
       public IActionResult Index()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(UserTable user)
        {
            var existingUser=_context.UserTables.FirstOrDefault(u=>u.UserName == user.UserName && u.Password==user.Password);
            if(existingUser != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Username or password");
            return View();
        }

    }
}
