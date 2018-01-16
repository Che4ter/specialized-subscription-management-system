using System.ComponentModel.DataAnnotations;

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