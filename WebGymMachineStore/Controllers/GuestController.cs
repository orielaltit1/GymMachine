using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModel;
using System.IO;
using System.Net;
using WebApiClient;

namespace WebGymMachineStore.Controllers
{
    public class GuestController : Controller
    {
        [HttpGet]
        public IActionResult HomePage()//html
        {
            return View();
        }

        [HttpGet]
        public IActionResult MachineCatalog(string selectedBrandId = "0", string sort = "-1")
        {   // 1. Get data from WebService
            WebClient<MachineCatalogeViewModel> Client = new WebClient<MachineCatalogeViewModel>(); 
            Client.Schema = "http"; 
            Client.Host = "localhost"; 
            Client.Port = 5086; 
            Client.Path = "Api/Guest/GetMachineCatalog"; 
            if(selectedBrandId != "0")
            {
                Client.AddParameter("selectedBrandId", selectedBrandId);
            }
            if(sort != "-1")
            {
                Client.AddParameter("sort", sort);
            }
            MachineCatalogeViewModel catalogeViewModel = Client.Get();
            return View(catalogeViewModel); 
        }

        [HttpGet]
        public IActionResult ProductPage(string id)
        {
            WebClient<MachineViewModel> client = new WebClient<MachineViewModel>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5086;
            client.Path = "Api/Guest/GetMachineView";
            client.AddParameter("id", id);
            MachineViewModel machineView = client.Get(); 
            return View(machineView);
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            WebClient<RegitrationViewModel> Client = new WebClient<RegitrationViewModel>();
            Client.Schema = "http";
            Client.Host = "localhost";
            Client.Port = 5138;
            Client.Path = "Api/Guest/GetCities";
            RegitrationViewModel list = Client.Get();
            return View(list);
        }
        
        [HttpPost]
        public async Task<IActionResult> RegistrationClient(Client client, IFormFile file)//IFormFile formFile 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    WebClient<RegitrationViewModel> webClient =
                        new WebClient<RegitrationViewModel>();

                    webClient.Schema = "http";
                    webClient.Host = "localhost";
                    webClient.Port = 5138;
                    webClient.Path = "Api/Guest/GetCities";

                    RegitrationViewModel model = webClient.Get();
                    model.Client = client;

                    if (file == null || file.Length == 0)
                    {
                        ModelState.AddModelError(
                            "Client.ClientPicture",
                            "Please Enter a Picture");
                    }

                    return View("Registration", model);
                }

                // העלאת תמונה
                if (file != null && file.Length > 0)
                {
                    // בדיקת סוג קובץ
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                    string extension =
                        Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ViewBag.Message = "Only image files are allowed";
                        return View(GetRegitrationViewModel(client));
                    }

                    string fileName = client.ClientId + extension;

                    string folderPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/images/clients"
                    );

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fullPath = Path.Combine(folderPath, fileName);

                    using (FileStream stream =
                           new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    //client.ClientPicture = "wwwroot/images/clients" + fileName;
                    client.ClientPicture = fileName;
                }

                WebClient<Client> user = new WebClient<Client>();

                user.Schema = "http";
                user.Host = "localhost";
                user.Port = 5138;
                user.Path = "Api/Guest/Registration";

                bool ok = user.Post(client);

                if (ok)
                {
                    HttpContext.Session.SetString(
                        "clientId",
                        client.ClientId
                    );
                    
                    return RedirectToAction("Profile", "Client");
                }

                ViewBag.Message = "Registration failed. Try again";

                return View(GetRegitrationViewModel(client));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                return View(GetRegitrationViewModel(client));
            }
        }

        private RegitrationViewModel GetRegitrationViewModel(Client client)
        {
            WebClient<RegitrationViewModel> Client = new WebClient<RegitrationViewModel>();
            Client.Schema = "http";
            Client.Host = "localhost";
            Client.Port = 5138;
            Client.Path = "Api/Guest/GetCities";
            RegitrationViewModel list = Client.Get();
            list.Client = client;
            return list;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View("LoginPage", loginViewModel);
        }
        [HttpPost]

        public IActionResult LoginClient(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View("LoginPage", loginViewModel);
            }
            WebClient<string> Client = new WebClient<string>();
            Client.Schema = "http";
            Client.Host = "localhost";
            Client.Port = 5138;
            Client.Path = "Api/Guest/Login";
            Client.AddParameter("email", loginViewModel.Email);
            Client.AddParameter("password", loginViewModel.Password);
            string id = Client.Get();
            if (id != null && id != "fail")
            {
                HttpContext.Session.SetString("clientId", id);

                return RedirectToAction(
                    "ClientHomePage",
                    "Client"
                );
            }
            ViewBag.Messege = "Email or password are incorrect";
            return View("LoginPage", loginViewModel);
        }
    }
}
