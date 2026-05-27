using GymMachineWS.DataAccessLayer;
using Models;
using System.Data;

namespace GymMachineWS
{
    public class CartItemRepository : Repository, IRepository<CartItem>
    {
        public CartItemRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {
        }

        public bool Create(CartItem item)
        {
            string sql = $@"INSERT INTO [OrderItem]
                            (
                            MachineId, OrderId,
                            Price, Amount
                            )
                            VALUES(@MachineId, @OrderId, @Price, @Amount)";
            this.dbContext.AddParamter("@MachineId", item.MachineId);
            this.dbContext.AddParamter("@OrderId", item.OrderId);
            this.dbContext.AddParamter("@Price", item.Price);
            this.dbContext.AddParamter("@Amount", item.Amount);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool DeleteItem(string machineId, int orderId)
        {
            string sql = $@"DELETE FROM [OrderItem]
                            WHERE MachineId = @MachineId
                            AND OrderId = @OrderId";
            this.dbContext.AddParamter("@MachineId", machineId);
            this.dbContext.AddParamter("@OrderId", orderId);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<CartItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public CartItem GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(CartItem item)
        {
            string sql = $@"UPDATE [OrderItem]
                            SET Price = @price,
                            Amount = @amount
                            WHERE OrderId = @orderId
                            AND MachineId = @machineId";
            this.dbContext.AddParamter("@price", item.Price.ToString());
            this.dbContext.AddParamter("@amount", item.Amount.ToString());
            this.dbContext.AddParamter("@orderId", item.OrderId.ToString());
            this.dbContext.AddParamter("@machineId", item.MachineId);
            return this.dbContext.Update(sql) > 0;
        }

        public List<CartItem> GetCartItems(string orderId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            string sql = $@"SELECT
                                *
                            FROM
                                OrderItem
                            WHERE
                                OrderId = @orderId;";
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

        public CartItem GetItem(string machineId, int orderId)
        {
            
            string sql =
                        @"SELECT *
                          FROM OrderItem
                          WHERE MachineId = @machineId
                          AND OrderId = @orderId";
            this.dbContext.AddParamter("@orderId", orderId.ToString());
            this.dbContext.AddParamter("@machineId", machineId);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                if (reader.Read())
                {
                    return this.factoryModles
                        .CartItemCreator
                        .CreateMoldel(reader);
                }

                return null;
            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
