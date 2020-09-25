using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        [Required(ErrorMessage = "Musisz podać nazwę promocji, max 100 znaków")]
        [MaxLength(100)]
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public bool Hidden { get; set; }
        [Required(ErrorMessage = "Musisz podać kod aktywujący promocji, max 50 znaków")]
        [MaxLength(50)]
        public string Code { get; set; }

        public virtual List<PromotionProduct> PromotionProducts { get; set; }
    }
}