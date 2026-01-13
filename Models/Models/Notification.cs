using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Notification : Model
    {
        int notificationId;
        int clientId;
        string notificationText;
        string createdDate;
        bool readStatus;

        
        public int NotificationId
        {
            get { return notificationId; }
            set { notificationId = value;}
        }
        [Required(ErrorMessage ="Please Enter Text")]
        public int ClientId
        {
            get { return clientId; }
            set {
                clientId = value;
                ValidateProperty(value, "clientId");
            }
        }
        [Required(ErrorMessage = "Please Enter Text")]
        public string NotificationText
        {
            get { return notificationText; }
            set {notificationText = value;
                ValidateProperty(value, "NotificationText");
            }
        }
        [Required(ErrorMessage = "Please Enter Text")]
        public string CreatedDate
        {
            get { return createdDate; }
            set 
            {
                createdDate = value;
                ValidateProperty(value, "CreatedDate");
            }
        }

        public bool ReadStatus
        {
            get { return readStatus; }
            set
            {
                readStatus = value;
                ValidateProperty(value, "ReadStatus");
            }
        }
    }
}
