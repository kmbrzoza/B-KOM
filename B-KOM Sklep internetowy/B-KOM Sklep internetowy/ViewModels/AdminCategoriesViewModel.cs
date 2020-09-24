using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class AdminCategoriesViewModel
    {
        public List<MainCategory> MainCategories { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}