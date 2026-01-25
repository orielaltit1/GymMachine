using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental;
using Models;


namespace WebGymMachineStore.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult ClientHomePage()
        {
            if (HttpContext.Session.GetString("ClientId") == null)
            {
                return RedirectToAction("LoginPage", "Guest");
            }

            return View();
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("ClientId") == null)
            {
                return RedirectToAction("LoginPage", "Guest");
            }

            Client model = new Client();
            {
                
                
            };

            return View(model);
        }
    }
}
