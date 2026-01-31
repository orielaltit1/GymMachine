using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client : Model
    {
        string clientFirstName;
        string clientLastName;
        string clientId;
        string clientGender;
        string clientEmail;
        string clientPassword;
        string clientPicture;
        string clientAdress;
        int cityId;
        string? clientSalt;

        [Required(ErrorMessage ="Please Enter First Name")]
        [StringLength(15,MinimumLength = 2,ErrorMessage ="Client Name Cant Be Longer Then 15 And Less Then 2")]
        [FirstLetterCapitalAttribut(ErrorMessage = "First Letter Must Be Capital Letter")]
        public string ClientFirstName
        {
            get { return clientFirstName; }
            set { clientFirstName = value;
                ValidateProperty(value, "ClientFirstName");
            }
        }
        [Required(ErrorMessage = "Please Enter Last Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Client Last Name Cant Be Longer Then 15 And Less Then 2")]
        [FirstLetterCapitalAttribut(ErrorMessage = "First Letter Must Be Capital Letter")]
        public string ClientLastName
        {
            get { return clientLastName; }
            set { clientLastName = value;
                ValidateProperty(value, "ClientLastName");
            }
        }

        [RegularExpression("^\\d+$", ErrorMessage = "Please Enter Only Numbers")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Your ID Must Be 9 Numbers Long")]
        public string ClientId
        {
            get { return clientId; }
            set { clientId = value;
                ValidateProperty(value, "ClientId");
            }
        }

        [Required(ErrorMessage = "Please Enter Your Gender")]
        public string ClientGender
        {
            get { return clientGender; }
            set { clientGender = value;
                ValidateProperty(value, "ClientGender");
            }
        }

        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address Format")]
        public string ClientEmail
        {
            get { return clientEmail; }
            set { clientEmail = value;
                ValidateProperty(value, "ClientEmail");
            }
        }
        [Required(ErrorMessage = "Please Enter Your Password")]
        [StringLength(10,MinimumLength = 6, ErrorMessage = "Please Enter At least 6 Chars")]
        public string ClientPassword
        {
            get { return clientPassword; }
            set { clientPassword = value; 
                ValidateProperty(value, "ClientPassword");
            }
        }
       
        public string? ClientPicture
        {
            get { return clientPicture; }
            set { clientPicture = value;}
        }
        [Required(ErrorMessage = "Please Enter Your Adress")]
        public string ClientAdress
        {
            get { return clientAdress; }
            set { clientAdress = value;
                ValidateProperty(value, "ClientAdress");
            }
        }
        [Required(ErrorMessage = "Please Enter Your City")]
        public int CityId
        {
            get { return cityId; }
            set {
                cityId = value;
                ValidateProperty(value, "CityId");
            }            
        }

        public string? ClientSalt { get; set; }
        



    }
}
