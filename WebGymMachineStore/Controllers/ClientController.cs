using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModel;
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
            Response.Headers["Cache-Control"] = "no-store";
            string clientIdStr = HttpContext.Session.GetString("ClientId");

            if (clientIdStr == null)
            {
                return RedirectToAction("LoginPage", "Guest");
            }
            int clientId = int.Parse(clientIdStr);
            WebClient<Client> webClient = new WebClient<Client>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;// web service port
            webClient.Path = $"Api/Client/{clientId}";
            Client client = webClient.Get();
            return View("Profile",client);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Headers["Cache-Control"] = "no-store";
            HttpContext.Session.Clear(); // מוחק את כל ה־session
            return RedirectToAction("HomePage", "Guest");
        }
        [HttpGet]
        public IActionResult Cart() 
        {
            string clientIdStr = HttpContext.Session.GetString("ClientId");

            if (clientIdStr == null)
            {
                return RedirectToAction("LoginPage", "Guest");
            }

            int clientId = int.Parse(clientIdStr);

            // מביא Order פתוחה
            WebClient<Order> orderClient = new WebClient<Order>();
            orderClient.Schema = "http";
            orderClient.Host = "localhost";
            orderClient.Port = 5138;
            orderClient.Path = $"Api/Client/GetOpenOrder/{clientId}";

            Order order = orderClient.Get();

            if (order == null)
            {
                return View(new List<LinkMachineOrder>());
            }

        }

    }
}
