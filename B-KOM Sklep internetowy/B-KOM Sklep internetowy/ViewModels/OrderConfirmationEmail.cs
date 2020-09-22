using B_KOM_Sklep_internetowy.Models;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class OrderConfirmationEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal OrderValue { get; set; }
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}