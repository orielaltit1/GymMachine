using GymMachineWS.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModel;

namespace GymMachineWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        RepositoryUnitOfWork repositoryUnitOfWork;
        public ClientController()
        {
            this.repositoryUnitOfWork = new RepositoryUnitOfWork();
        }
        [HttpPost]
        public string Login(string email, string password)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                string clientId = this.repositoryUnitOfWork.ClientRepository.Login(email ,password);
                return clientId;
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

        [HttpGet]
        public List<CartItem> GetCart(string orderId)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                return this.repositoryUnitOfWork.OrderRepository.GetCartItems(orderId);
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
