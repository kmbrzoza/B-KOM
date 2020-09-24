using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę Głównej kategorii")]
        [StringLength(100)]
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}