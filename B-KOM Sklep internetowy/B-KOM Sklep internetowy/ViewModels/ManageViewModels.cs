using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class ManageCredentialViewModel
    {
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public B_KOM_Sklep_internetowy.Controllers.ManageController.ManageMessageId? Message { get; set; }
        public UserData UserData { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Stare hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hasło musi zawieriać co najmniej 6 znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Podane hasła nie są takie same!")]
        public string ConfirmNewPassword { get; set; }
    }
}