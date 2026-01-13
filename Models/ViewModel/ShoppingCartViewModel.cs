using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public Order Order { get; set; }

        public List<GymMachine> Machines { get; set; }
    }
}
