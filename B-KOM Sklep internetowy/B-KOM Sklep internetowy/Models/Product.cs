using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Wprowadź nazwę produktu")]
        [StringLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImgPath { get; set; }
        public DateTime AddDate { get; set; }
        public bool Hidden { get; set; }
        public bool Recommended { get; set; }
        public bool Bestseller { get; set; }
        public bool Promo { get; set; }
        public decimal PromoPrice { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
    }
}