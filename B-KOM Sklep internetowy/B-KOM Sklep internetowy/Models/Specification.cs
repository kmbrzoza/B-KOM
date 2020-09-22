using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Specification
    {
        public int SpecificationId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
    }
}