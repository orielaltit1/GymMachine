using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class RegitrationViewModel
    {
        public Client Client { get; set; }
        public List<City> Cities { get; set; }
    }
}
