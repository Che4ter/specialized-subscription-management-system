using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Models.ManageViewModels
{
    public class EmployeeAccountViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("Benutzername")]
        public string Email { get; set; }
    }
}
