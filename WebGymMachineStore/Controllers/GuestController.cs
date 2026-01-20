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
            // doofdogodigl
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
        public IActionResult RegistrationClient(Client client)//IFormFile formFile 
        {
            if(ModelState.IsValid == false)
            {
                WebClient<RegitrationViewModel> Client = new WebClient<RegitrationViewModel>();
                Client.Schema = "http";
                Client.Host = "localhost";
                Client.Port = 5138;
                Client.Path = "Api/Guest/GetCities";
                RegitrationViewModel list = Client.Get();
                list.Client = client;
                return View("Registration", list);
            }
            WebClient<Client> user = new WebClient<Client>();
            user.Schema = "http";
            user.Host = "localhost";
            user.Port = 5138;
            user.Path = "Api/Guest/Registration";
            bool ok = user.Post(client);
            if (ok == true)
            {
                 HttpContext.Session.SetString("clientId", client.ClientId);// session is an object - hashtable 
                return RedirectToAction("HomePage", "Guest");
            }
            ViewBag.Messege = "Registration faild. Try again";
            return View(GetRegitrationViewModel(client));
            
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
            if (id != "")
            {
                HttpContext.Session.SetString("ClientId", id);// session is an object - hashtable 
                return RedirectToAction("HomePage", "Guest");
            }
            ViewBag.Messege = "Email or password are incorrect";
            return View("LoginPage", loginViewModel);
        }




    }
}
