using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuelles Passwort")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Zeichen lang sein", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Neues")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bestätigung des neuen Passwortes")]
        [Compare("NewPassword", ErrorMessage = "Die Passwörter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }

    }
}
