using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class AdminPromotionDetailsViewModel
    {
        public Promotion Promotion { get; set; }
        public PromotionProduct PromotionProduct { get; set; }
        public List<ProductPromotionDTO> ActualProductsList { get; set; }
    }
}