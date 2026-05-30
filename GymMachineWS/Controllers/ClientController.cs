using GymMachineWS.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModel;


namespace GymMachineWS.Controllers
{
    [Route("Api/[controller]/[action]")]
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
                return this.repositoryUnitOfWork.CartItemRepository.GetCartItems(orderId);
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

        [HttpGet("{machineId}")]
        public GymMachine GetMachine(string machineId)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                return this.repositoryUnitOfWork.GymMachineRepository.GetById(machineId);
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
                return null;
            }
            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();
            }
        }

        [HttpPost]
        public IActionResult Payment(CheckOutDto dto)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();

                this.repositoryUnitOfWork
                    .OrderRepository
                    .CheckOut(dto.ClientId);

                return Ok();
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

        [HttpGet]
        public bool DeleteItem(string machineId, int orderId)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                return this.repositoryUnitOfWork.CartItemRepository.DeleteItem(machineId, orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
            finally
            {
                this.repositoryUnitOfWork.DisconnectDb();
            }
        }

        //Creates new Order
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody]Order order)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                this.repositoryUnitOfWork.OrderRepository.Create(order);
                return Ok(order);
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

        [HttpPost]
        public IActionResult UpdateClient(UpdateProfileDto dto)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();

                this.repositoryUnitOfWork.ClientRepository
                    .UpdateEmailAndAdress(dto.ClientId,
                                          dto.ClientEmail,
                                          dto.ClientAdress);

                return Ok();
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



        [HttpPost]
        public ActionResult<CartItem> UpdateCartItem(CartItem cartItem)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                this.repositoryUnitOfWork.CartItemRepository.Update(cartItem);
                return Ok(cartItem);
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

        [HttpPost]
        public ActionResult AddCartItem(CartItem item)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();

                if (item == null)
                    return BadRequest();

                this.repositoryUnitOfWork.CartItemRepository.Create(item);

                return Ok();
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

        [HttpGet("{clientId}")]
        public ActionResult<Order> GetOpenOrder(int clientId)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();

                Order order = this.repositoryUnitOfWork
                    .OrderRepository
                    .GetOpenOrderByClientId(clientId);

                if (order == null)
                    return NotFound(); // Returns a 404 status code

                return Ok(order);// Returns a 200 status code with data
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

        [HttpGet("{clientId}")]
        public ActionResult<Client> GetClientById(string clientId)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();
                return this.repositoryUnitOfWork.ClientRepository.GetById(clientId);
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

        [HttpGet("{orderId}")]
        public ActionResult<List<CartItem>> GetCartItem(string orderId)
        {
            try
            {
                this.repositoryUnitOfWork.ConnectDb();

                List<CartItem> items =
                    this.repositoryUnitOfWork
                    .CartItemRepository
                    .GetCartItems(orderId);

                return Ok(items);
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
