using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    // DTO = Data Transfer Object
    // המחלקה הזאת נועדה להעביר רק את הנתונים
    // שצריך בשביל עדכון פרופיל
    // ולא את כל אובייקט Client

    public class UpdateProfileDto
    {
        // מזהה הלקוח
        // כדי לדעת איזה משתמש לעדכן במסד הנתונים
        public string ClientId { get; set; }

        // האימייל החדש שהמשתמש הכניס
        public string ClientEmail { get; set; }

        // הכתובת החדשה שהמשתמש הכניס
        public string ClientAdress { get; set; }
    }
}
