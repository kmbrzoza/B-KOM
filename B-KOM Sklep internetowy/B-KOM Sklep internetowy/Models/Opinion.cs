using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Opinion
    {
        public int Opinionid { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string UserName { get; set; }
        public int StarsValue { get; set; }
        public string OpinionText { get; set; }
        public DateTime DateTime { get; set; }
        public bool Accepted { get; set; }

        public virtual Product Product { get; set; }
    }
}