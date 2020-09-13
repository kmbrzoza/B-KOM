using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Wprowadź adres e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Wprowadź adres e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [StringLength(60, ErrorMessage = "Hasło musi zawierać conajmniej 6 znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string ConfirmPassowrd { get; set; }
    }

}