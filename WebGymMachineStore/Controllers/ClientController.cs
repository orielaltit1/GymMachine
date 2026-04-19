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
            return View("Profile", client);
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

        [HttpGet]
        public IActionResult AddToCart()
        {

        }

       //[HttpGet]
        //public Task<IActionResult>  JsonChat()
        //{
        //    string apiKey = "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";
        //    string model = "openrouter/free";
        //    string url = "https://openrouter.ai/api/v1/chat/completions/";
        //    WebClient<Order> orderClient = new WebClient<Order>();
        //    WebClient.AiWebClient aiWebClient = new WebClient.AiWebClient(apiKey, model);
        //    aiWebClient.Scheme = "https";
        //    aiWebClient.Host = "openrouter.ai";
        //    aiWebClient.Path = "api/v1/chat/completions";
        //    List<WebClient.Message> chatHistory = new List<WebClient.Message>();
        //    chatHistory.Add(new WebClient.Message { Role = "system", Content = "You are my assistent" });


        //    Console.Write("You : >> ");
        //    string userInput = @"You are a strict data assistant.
        //                 Given the question, you MUST answer with a JSON object that contains all type gym machines.
        //                 You MUST return ONLY valid JSON.
        //                 With realy links to pictures of the machines.
        //                 Do not add any greetings, explanations, or markdown formatting (like ```json).
        //                 Your output must exactly match this structure:
        //                  {
        //                      ""Machines""
        //                        [
        //                           { ""Name"": ""Power Cage or Squat Rack"",
        //                           ""Picture"": ""https://cdn.shopify.com/s/files/1/0252/3155/6686/files/Squat_Rack_3_600x600.jpg?v=1719216632 "" }
        //                      ]
        //                    }";
        //    chatHistory.Add(new WebClient.Message { Role = "user", Content = userInput });
        //    object responseFormat = new { type = "json_object" };
        //    //  = new
        //    // {
        //    //    type = "json_schema",
        //    //    json_schema = new
        //    //    {
        //    //        name = "top_countries_schema",
        //    //        strict = true, // מבטיח שהמודל לא יסטה מהמבנה
        //    //        schema = new
        //    //        {
        //    //            type = "object",
        //    //            properties = new
        //    //            {
        //    //                // המאפיין הראשי שלנו הוא מערך של מדינות
        //    //                Countries = new
        //    //                {
        //    //                    type = "array",
        //    //                    description = "List of top 10 most populated countries",
        //    //                    items = new // איך נראה כל פריט במערך?
        //    //                    {
        //    //                        type = "object",
        //    //                        properties = new
        //    //                        {
        //    //                            Name = new { type = "string", description = "שם המדינה בעברית" },
        //    //                            Population = new { type = "integer", description = "מספר התושבים הכולל" }
        //    //                        },
        //    //                        required = new[] { "Name", "Population" }, // חובה להחזיר את שני השדות
        //    //                        additionalProperties = false // בלי שדות שה-AI ממציא
        //    //                    }
        //    //                }
        //    //            },
        //    //            required = new[] { "Countries" }, // חובה להחזיר את המערך עצמו
        //    //            additionalProperties = false
        //    //        }
        //    //    }
        //    //};
        //    // aiWebClient.AddJsonSchema(responseFormat);
        //    var response = await aiWebClient.GetChatRequest(chatHistory);
        //    MachinesResponse machines = JsonSerializer.Deserialize<MachinesResponse>(response.Content);
        //    // Console.WriteLine($"AI : >> {response.Content}");
        //    foreach (var machine in machines.Machines)
        //    {
        //        Console.WriteLine($"Country: {machine.Name}, Population: {machine.Picture}");
        //    }
        //    chatHistory.Add(response);
        //}
    }



}
