using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class CartItem
    {
       public string MachineId { get; set; }
       public string OrderId { get; set; }
       public string Price { get; set; }
       public string Amount { get; set; }

    }
}
