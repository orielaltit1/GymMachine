using GymMachineWS.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModel;

namespace GymMachineWS.Controllers
{
    [Route("Api/[controller]")]
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
        
        [HttpGet("{id}")]
        public ActionResult<Client> GetData(string id)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                Client dataClient = this.repositoryUnitOfWork.ClientRepository.GetById(id);

                if (dataClient == null)
                    return NotFound();

                return Ok(dataClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();
            }
        }
    }
}
