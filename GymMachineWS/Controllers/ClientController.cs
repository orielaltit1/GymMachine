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
                    .OrderRepository
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

        //[HttpPost("{clientId}")]
        //public ActionResult<bool> AddItemToCart([FromBody] CartItem item, int clientId)
        //{
        //    try
        //    {
        //        this.repositoryUnitOfWork.ConnectDb();
                
        //        int orderId = repositoryUnitOfWork.OrderRepository.GetOpenOrderByClientId(clientId);


        //    }
        //}

        //[HttpPost]
        //public bool AddToCart(int clientId, int machineId)
        //{
        //    try
        //    {
        //        this.repositoryUnitOfWork.ConnectDb();

        //        // 🔹 1. בודק אם יש הזמנה פתוחה
        //        Order order = this.repositoryUnitOfWork.OrderRepository
        //            .GetAll()
        //            .FirstOrDefault(o => o.ClientId == clientId && o.orderPayet);

        //        // 🔹 2. אם אין – יוצר חדשה
        //        if (order == null)
        //        {
        //            order = new Order
        //            {
        //                ClientId = clientId,
        //                OrderDate = DateTime.Now,
        //                IsOpen = true
        //            };

        //            this.repositoryUnitOfWork.OrderRepository.Insert(order);
        //            this.repositoryUnitOfWork.Save();
        //        }

        //        // 🔹 3. בודק אם המוצר כבר בעגלה
        //        CartItem existingItem = this.repositoryUnitOfWork.CartItemRepository
        //            .GetAll()
        //            .FirstOrDefault(c => c.OrderId == order.OrderId && c.MachineId == machineId);

        //        if (existingItem != null)
        //        {
        //            // 🔹 4. אם כן – מגדיל כמות
        //            existingItem.Quantity++;
        //            this.repositoryUnitOfWork.CartItemRepository.Update(existingItem);
        //        }
        //        else
        //        {
        //            // 🔹 5. אם לא – מוסיף חדש
        //            CartItem newItem = new CartItem
        //            {
        //                OrderId = order.OrderId,
        //                MachineId = machineId,
        //                Quantity = 1
        //            };

        //            this.repositoryUnitOfWork.CartItemRepository.Insert(newItem);
        //        }

        //        this.repositoryUnitOfWork.Save();

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
