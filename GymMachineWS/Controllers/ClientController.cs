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
