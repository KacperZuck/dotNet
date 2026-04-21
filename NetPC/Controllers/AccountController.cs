using Microsoft.AspNetCore.Mvc;
using NetPC.Data;

namespace NetPC.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContext _context;

        public AccountController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            Console.WriteLine("LOGIN HIT");
            Console.WriteLine(Request.Path);
            Console.WriteLine(Request.Method);
            var user = _context.Contacts.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                TempData["LoginError"] = "Nie ma użytkownika";
                return RedirectToAction("Index", "Contact");
            }

            if (user.Password != password)
            {
                TempData["LoginError"] = "Złe hasło";
                return RedirectToAction("Index", "Contact");
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return RedirectToAction("Index", "Contact");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Contact");
        }
    }
}