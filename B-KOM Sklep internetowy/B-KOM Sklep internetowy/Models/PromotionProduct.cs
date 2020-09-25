using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class PromotionProduct
    {
        public int PromotionProductId { get; set; }
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public decimal PromotionPrice { get; set; }
        [Required]
        public int Amount { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual Product Product { get; set; }
    }
}