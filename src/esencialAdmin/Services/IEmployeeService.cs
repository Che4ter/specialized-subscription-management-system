using esencialAdmin.Models.CustomerViewModels;
using esencialAdmin.Models.EmployeeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public interface IEmployeeService
    {
        System.Threading.Tasks.Task<bool> deleteEmployee(string username);
        bool isUsernameUnique(string username);
        JsonResult loadEmployeeDataTable(HttpRequest Request);
        EmployeeEditViewModel loadEmployeeEditModel(string username);
        System.Threading.Tasks.Task<bool> updateEmployee(EmployeeEditViewModel employeeToUpdate);

        List<EmployeeRolesViewModel> getAvailableRoles();
    }
}