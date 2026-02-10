using GymMachineWS.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModel;

namespace GymMachineWS.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        RepositoryUnitOfWork repositoryUnitOfWork;
        public AdminController()
        {
            this.repositoryUnitOfWork = new RepositoryUnitOfWork();
        }

        [HttpGet]
        public List<AdminMachineViewModel> GetMachines()
        {
            
            List<AdminMachineViewModel> machines = new List<AdminMachineViewModel>();
            try
            {
                this.repositoryUnitOfWork.ConnectDb(); //Open Connection
                List<GymMachine> machine = this.repositoryUnitOfWork.GymMachineRepository.GetAll();
                foreach(GymMachine gymMachine in machine)
                {
                    AdminMachineViewModel machineViewModel = new AdminMachineViewModel();
                    machineViewModel.Machine = gymMachine;
                    machineViewModel.Brand = this.repositoryUnitOfWork.GymMachineBrandRepository.GetById(gymMachine.BrandId);
                    machines.Add(machineViewModel);
                }
                return machines;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Console.WriteLine(error);
                return null;
            }
            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();//Close Connection
            }
        }
        
    }
}
