using B_KOM_Sklep_internetowy.Models;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class HangfirePostalMailService : IMailService
    {
        public void SendOrderConfirmationEmail(Order order)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("OrderConfirmationEmail", "Admin", new { orderId = order.OrderId }, HttpContext.Current.Request.Url.Scheme);
            
            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
        }
    }
}