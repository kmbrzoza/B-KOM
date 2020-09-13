using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}