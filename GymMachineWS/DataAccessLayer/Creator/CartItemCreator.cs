using Models;
using Models.Models;
using System.Data;

namespace GymMachineWS
{
    public class CartItemCreator : IModelCreator<CartItem>
    {
        public CartItem CreateMoldel(IDataReader reader)
        {
            return new CartItem()
            {
                 Amount = Convert.ToString(reader["Amount"]),
                 MachineId = Convert.ToString(reader["MachineId"]),
                 OrderId = Convert.ToString(reader["OrderId"]),
                 Price = Convert.ToString(reader["Price"]),
            };
        }
    }
}
