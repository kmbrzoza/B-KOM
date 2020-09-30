using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class PromotionHotDeal
    {
        public int PromotionHotDealId { get; set; }
        public int ProductId { get; set; }
        public decimal PromotionPrice { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        public int Amount { get; set; }
        public int AmountLeft { get; set; }
        public bool Active { get; set; }

        public virtual Product Product { get; set; }
    }
}