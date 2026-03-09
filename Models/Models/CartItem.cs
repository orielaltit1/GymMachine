using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CartItem
    {
       public string MachineId { get; set; }
       public string OrderId { get; set; }
       public decimal Price { get; set; }
       public int Amount { get; set; }

    }
}
