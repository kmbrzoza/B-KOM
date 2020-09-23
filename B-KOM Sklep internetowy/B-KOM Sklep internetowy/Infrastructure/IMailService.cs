using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public interface IMailService
    {
        void SendOrderConfirmationEmail(Order order);
    }
}
