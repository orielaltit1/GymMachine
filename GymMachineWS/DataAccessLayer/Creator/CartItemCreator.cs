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
                 Amount = Convert.ToUInt16(reader["Amount"]),
                 MachineId = Convert.ToString(reader["MachineId"]),
                 OrderId = Convert.ToInt16(reader["OrderId"]),
                 Price = Convert.ToDecimal(reader["Price"]),
            };
        }
    }
}
