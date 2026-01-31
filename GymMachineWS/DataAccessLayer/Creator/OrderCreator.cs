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
                OrderId = Convert.ToInt16(reader["OrderId"]),
                ClientId = Convert.ToInt16(reader["ClientId"]),
                OrderDate = Convert.ToString(reader["OrderDate"]),
                OrderPayet = Convert.ToBoolean(reader["OrderPayet"])
            };
            
        }
    }
}
