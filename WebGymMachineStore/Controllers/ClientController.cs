using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using WebApiClient;


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

        [HttpGet]
        public IActionResult Profile()
        {
            string clientIdStr = HttpContext.Session.GetString("ClientId");

            if (clientIdStr == null)
            {
                return RedirectToAction("LoginPage", "Guest");
            }
            int clientId = int.Parse(clientIdStr);
            WebClient<Client> webClient = new WebClient<Client>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = $"Api/Client/{clientId}";
            Client client = webClient.Get();
            return View("Profile",client);
        }
        
    }
}
