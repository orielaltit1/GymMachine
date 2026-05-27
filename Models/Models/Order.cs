using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        int clientId;
        int orderId;
        string orderDate;
        bool orderPayet;//אם ההזמנה שולמה או לא

        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public string OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        public bool OrderPayet
        {
            get { return orderPayet; }
            set { orderPayet = value; }
        }
    }
}
