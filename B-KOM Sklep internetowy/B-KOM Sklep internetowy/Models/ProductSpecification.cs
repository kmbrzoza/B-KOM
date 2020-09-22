using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class ProductSpecification
    {
        public int ProductSpecificationId { get; set; }
        public int SpecificationId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Value { get; set; }

        public virtual Specification Specification { get; set; }
        public virtual Product Product { get; set; }
    }
}