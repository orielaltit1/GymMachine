using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GymMachine:Model
    {
        string machineName;
        string machineDescription;
        string machineId;
        string machinePrice;
        string machineImage;
        string brandId;
        bool isActive;

        [Required(ErrorMessage = "Please Enter Machine Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Please Enter Only English Letters")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Machine Name Cant Be Longer Then 20 And Less Then 2")]
        public string MachineName 
        {  
            get { return machineName; } 
            set { machineName = value;
                  ValidateProperty(value, "MachineName");
            } 
        }

        public string MachineId
        {
            get { return machineId; }
            set { machineId = value; }
        }
        [RegularExpression("^\\d+$", ErrorMessage = "Please Enter Only Numbers")]
        public string MachinePrice
        {
            get { return machinePrice; }
            set { machinePrice = value;
                ValidateProperty(value, "MachinePrice");
            }
        }
        [Required(ErrorMessage = "Please Enter Image")]
        public string MachineImage
        {
            get { return machineImage; }
            set { machineImage = value;
                ValidateProperty(value, "MachineImage");
            }
        }
        [Required(ErrorMessage = "Please Enter Description")]
        public string MachineDescription
        {
            get { return machineDescription; }
            set { machineDescription = value;
                  ValidateProperty(value, "MachineDescription");
            }
        }
        public string BrandId
        {
            get { return brandId; }
            set { brandId = value;
                  ValidateProperty(value, "BrandId");
                }
        }

        public bool IsActive { get; set; }
    }
}
