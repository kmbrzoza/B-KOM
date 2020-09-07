using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Specification
    {
        public int SpecificationId { get; set; }
        public string Name { get; set; }

        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
    }
}