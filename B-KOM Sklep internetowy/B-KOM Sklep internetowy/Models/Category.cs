using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int MainCategoryId { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwę Kategorii")]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual MainCategory MainCategory { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}