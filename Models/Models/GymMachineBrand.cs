using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GymMachineBrand:Model
    {
        string brandName;
        int brandId;
        string brandLogo;

        [Required(ErrorMessage = "Please Enter Brand Name")]
        [RegularExpression("^[A-Za-z]+$",ErrorMessage ="Please Enter Only English Letters")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Brans Name Cant Be Longer Then 20 And Less Then 2")]
        public string BrandName 
        {
            get { return brandName; }
            set { brandName = value;
                ValidateProperty(value, "BrandName");
            }
        }
        [Required(ErrorMessage = "Please Enter ID")]
        [RegularExpression("^\\d+$", ErrorMessage = "Please Enter Only Numbers")]
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value;
                ValidateProperty(value, "BrandId");
            }
        }
        [Required(ErrorMessage = "Please Enter Image")]
        public string BrandLogo
        {
            get { return brandLogo; }
            set { brandLogo = value;
                ValidateProperty(value, "BrandLogo");
            }
        }
    }
}
