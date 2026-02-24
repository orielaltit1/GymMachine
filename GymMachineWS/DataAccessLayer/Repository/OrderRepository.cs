using Models;
using System.Data;

namespace GymMachineWS.DataAccessLayer
{
    public class OrderRepository : Repository, IRepository<Order>
    {
        public OrderRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {
        }

        public bool Create(Order item)
        {
            string sql = $@"INSERT INTO [Order]
                            ( 
                            ClientId, OrderId, OrderDate, OrderPayet
                            )
                            VALUES
                            (
                            @clientId, @orderId, @orderDate, @orderPayet
                            )";
            this.dbContext.AddParamter("@clientId", item.ClientId.ToString());
            this.dbContext.AddParamter("@orderId", item.OrderId.ToString());
            this.dbContext.AddParamter("@orderDate", item.OrderDate.ToString());
            this.dbContext.AddParamter("@orderPayet", item.OrderPayet.ToString());
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete FROM [Order]
                            WHERE ClientId = @clientID";
            this.dbContext.AddParamter("@clientID", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<Order> GetAll()
        {
            List<Order> order = new List<Order>();
            string sql = "SELECT * FROM [Order]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    order.Add(this.factoryModles.OrderCreator.CreateMoldel(reader));
                }
            }
            return order;
        }

        public Order GetById(string id)
        {
            string sql = $@"SELECT *  FROM [Order] where clientId=@ClientId";
            this.dbContext.AddParamter("@clientID", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModles.OrderCreator.CreateMoldel(reader);
            }
        }

        public bool Update(Order item)
        {
            string sql = $@"UPDATE [Order] SET ClientId=@clientId, OrderId=@orderId,
                            OrderDate=@orderDate, OrderPayet=@orderPayet
                            WHERE ClientId=@clientId";
            //לבדוק שאין INJECTION
            this.dbContext.AddParamter("@clientId", item.ClientId.ToString());
            this.dbContext.AddParamter("@orderId", item.OrderId.ToString());
            this.dbContext.AddParamter("@orderDate", item.OrderDate.ToString());
            this.dbContext.AddParamter("@orderPayet", item.OrderPayet.ToString());
            return this.dbContext.Update(sql) > 0;
        }
        public List<CartItem> GetCartItems(string orderId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            string sql = $@"SELECT LinkMachineOrder.MachineId, LinkMachineOrder.OrderId,
                             LinkMachineOrder.Price, LinkMachineOrder.Amount
                             FROM LinkMachineOrder
                             WHERE (((LinkMachineOrder.OrderId)=@orderId));";
            this.dbContext.AddParamter("@orderId", orderId);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    cartItems.Add(this.factoryModles.CartItemCreator.CreateMoldel(reader));
                }
            }
            return cartItems;
        }

        
    }
}
