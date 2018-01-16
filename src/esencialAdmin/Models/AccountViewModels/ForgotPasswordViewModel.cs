using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("E-Mail Adresse")]
        public string Email { get; set; }
    }
}