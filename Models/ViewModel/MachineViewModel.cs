using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MachineViewModel
    {
        public GymMachine Machine { get; set; }
        public int SelectAmount { get; set; }
    }
    public class AdminMachineViewModel
    {
        public GymMachine Machine { get; set; }
        public GymMachineBrand Brand { get; set; }
    }
}
