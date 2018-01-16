using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "E-Mail Adresse")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Neues Passwort")]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Zeichen lang sein", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passwort bestätigen")]
        [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}