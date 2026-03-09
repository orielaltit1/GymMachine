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
            webClient.Path = $"Api/Client/GetClientById/{clientId}";
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
                return View(new ShoppingCartViewModel
                {
                    Order = null,
                    Machines = new List<GymMachine>()
                });
            }

            // מביא את הפריטים בעגלה
            WebClient<List<CartItem>> cartClient = new WebClient<List<CartItem>>();
            cartClient.Schema = "http";
            cartClient.Host = "localhost";
            cartClient.Port = 5138;
            cartClient.Path = $"Api/Client/GetCartItem/{order.OrderId}";

            List<CartItem> cartItems = cartClient.Get();

            // כאן אתה צריך להביא את המכונות לפי ה-MachineId
            List<GymMachine> machines = new List<GymMachine>();

            foreach (CartItem item in cartItems)
            {
                WebClient<GymMachine> machineClient = new WebClient<GymMachine>();
                machineClient.Schema = "http";
                machineClient.Host = "localhost";
                machineClient.Port = 5138;
                machineClient.Path = $"Api/Client/GetMachine/{item.MachineId}";

                GymMachine machine = machineClient.Get();
                machines.Add(machine);
            }

            ShoppingCartViewModel vm = new ShoppingCartViewModel
            {
                Order = order,
                Machines = machines
            };

            return View(vm);
        }

    }
}
