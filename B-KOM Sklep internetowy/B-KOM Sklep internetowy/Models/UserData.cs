using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Podaj prawidłowy adres e-mail")]
        public string Email { get; set; }
    }
}