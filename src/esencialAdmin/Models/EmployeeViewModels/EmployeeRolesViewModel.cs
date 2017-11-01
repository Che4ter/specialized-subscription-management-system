using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.EmployeeViewModels
{
    public class EmployeeRolesViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Rolle")]
        public string Role { get; set; }

    }
}
