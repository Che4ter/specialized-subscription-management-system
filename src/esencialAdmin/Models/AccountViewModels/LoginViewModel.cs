using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [DisplayName("E-Mail Adresse")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string Password { get; set; }

        [Display(Name = "Merken?")]
        public bool RememberMe { get; set; }
    }
}