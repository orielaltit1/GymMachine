using GymMachineWS.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModel;
using System.Reflection.PortableExecutable;

namespace GymMachineWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        RepositoryUnitOfWork repositoryUnitOfWork;
        public GuestController()
        {
            this.repositoryUnitOfWork = new RepositoryUnitOfWork();
        }

        [HttpGet]
        public MachineCatalogeViewModel GetMachineCatalog(string selectedBrandId="0", string sort="-1")
        {


            MachineCatalogeViewModel machineCatalogeViewModel = new MachineCatalogeViewModel();
            try 
            {
                this.repositoryUnitOfWork.ConnectDb(); //Open Connection
                if (selectedBrandId == "0" && sort == "-1")//   
                    machineCatalogeViewModel.Machines = this.repositoryUnitOfWork.GymMachineRepository.GetAll();
                else if(selectedBrandId != "0" && sort == "-1")//
                    machineCatalogeViewModel.Machines = this.repositoryUnitOfWork.GymMachineRepository.GetMachineByBrand(selectedBrandId);
                else if(selectedBrandId != "0" && sort != "-1")//
                { 
                   machineCatalogeViewModel.Machines = this.repositoryUnitOfWork.GymMachineRepository.GetMachineByBrand(selectedBrandId,sort);
                   
                }
                else if(selectedBrandId == "0" && sort != "-1")//
                {
                   machineCatalogeViewModel.Machines = this.repositoryUnitOfWork.GymMachineRepository.GetAll(sort);
                }
                machineCatalogeViewModel.Brands = this.repositoryUnitOfWork.GymMachineBrandRepository.GetAll();
                machineCatalogeViewModel.SelectedBrandId = selectedBrandId;
                machineCatalogeViewModel.Sort = sort;
                return machineCatalogeViewModel;
            }
            catch(Exception ex)
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

        [HttpPost]
        public bool Registration([FromBody] Client client)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                bool result = this.repositoryUnitOfWork.ClientRepository.Create(client);
                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();//Close Connection
            }
        }

        [HttpGet]
        public RegitrationViewModel GetCities()
        {
            List<City> cities = new List<City>();
            RegitrationViewModel viewModel = new RegitrationViewModel();
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                viewModel.Client = null;
                viewModel.Cities = this.repositoryUnitOfWork.CityRepository.GetAll();
                return viewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();
            }
            

        }

        [HttpGet]
        public MachineViewModel GetMachineView(string id)
        {
            MachineViewModel machineViewModel = new MachineViewModel();
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                machineViewModel.Machine = repositoryUnitOfWork.GymMachineRepository.GetById(id);
                machineViewModel.SelectAmount = 1;
                return machineViewModel;         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();
            }
        }

    }
}
