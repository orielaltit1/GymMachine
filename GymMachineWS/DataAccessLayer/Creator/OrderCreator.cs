using System.Data;
using Models;


namespace GymMachineWS.DataAccessLayer
{
    public class OrderCreator : IModelCreator<Order>
    {
        public Order CreateMoldel(IDataReader reader)
        {
            return new Order()
            {
                OrderId = Convert.ToInt32(reader["OrderId"]),
                ClientId = Convert.ToInt32(reader["ClientId"]),
                OrderDate = Convert.ToString(reader["OrderDate"]),
                OrderPayet = Convert.ToBoolean(reader["OrderPayet"])
            };
            
        }
    }
}
