using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class AdminHotDealViewModel
    {
        public PromotionHotDeal newHotDeal { get; set; }
        public List<PromotionHotDeal> listHotDeals { get; set; }
    }
}