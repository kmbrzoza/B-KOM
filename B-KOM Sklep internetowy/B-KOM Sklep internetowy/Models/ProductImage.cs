using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ImgPath { get; set; }
        public bool MainImg { get; set; }

        public virtual Product Product { get; set; }
    }
}