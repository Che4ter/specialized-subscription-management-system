using System.ComponentModel;

namespace esencialAdmin.Models.ManageViewModels
{
    public class EmployeeAccountViewModel
    {
        [DisplayName("Benutzername")]
        public string Email { get; set; }
    }
}