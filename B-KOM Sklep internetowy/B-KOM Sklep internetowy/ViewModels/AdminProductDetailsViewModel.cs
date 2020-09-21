using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class AdminProductDetailsViewModel
    {
        public Product Product { get; set; }
        public List<MainCategory> MainCategories { get; set; }
    }
}