using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class PostalMailService : IMailService
    {
        public void SendOrderConfirmationEmail(Order order)
        {
            OrderConfirmationEmail email = new OrderConfirmationEmail();
            email.To = order.Email;
            email.From = Consts.shopEmail;
            email.OrderValue = order.OrderValue;
            email.OrderId = order.OrderId;
            email.OrderItems = order.OrderItems;

            email.Send();
        }
    }
}