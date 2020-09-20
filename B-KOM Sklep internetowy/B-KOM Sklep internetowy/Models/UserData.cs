using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class UserData
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(6)]
        public string PostCode { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Podaj prawidłowy adres e-mail")]
        public string Email { get; set; }
    }
}