using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MachineCatalogeViewModel
    {
        public List<GymMachine> Machines { get; set; }

        public List<GymMachineBrand>? Brands { get; set; }

        public string SelectedBrandId { get; set; }

        public string Sort { get; set; }
        

    }
}
    