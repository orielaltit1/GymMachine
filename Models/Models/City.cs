using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class City : Model
    {
        int cityId;
        string cityName;

        
        public int CityId 
        { 
            get { return cityId; }
            set { cityId = value; }
        }

        [Required(ErrorMessage = "Please Enter City Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Name Must Be 2-30 Characters")]
        [FirstLetterCapitalAttribut(ErrorMessage = "First Letter Of City Must Be Capital Letter")]
        public string CityName
        {
            get { return cityName; }
            set { cityName = value;
                ValidateProperty(value, "CityName");
            }
        }
    }
}
