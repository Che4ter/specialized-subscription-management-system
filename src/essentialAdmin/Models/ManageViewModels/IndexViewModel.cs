using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public EmployeeAccountViewModel employeeAccountViewModel { get; set; }

        public ChangePasswordViewModel changePasswordViewModel { get; set; }
    }
}
