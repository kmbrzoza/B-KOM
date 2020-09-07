using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}