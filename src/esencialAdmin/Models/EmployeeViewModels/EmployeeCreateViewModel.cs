using esencialAdmin.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.EmployeeViewModels
{
    public class EmployeeCreateViewModel
    {
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "E-Mail Adresse")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Zeichen lang sein", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passwort bestätigen")]
        [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Rolle")]
        public string Role { get; set; }

        [Display(Name = "Rolle")]
        public virtual List<EmployeeRolesViewModel> EmployeeRoles { get; set; }

    }
}
