using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProductDTO> Recommended { get; set; }
        public IEnumerable<ProductDTO> Bestsellers { get; set; }
        public List<Promotion> Promotions { get; set; }
    }
}