using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using essentialAdmin.Data.Models;
using essentialAdmin.Models.CustomerViewModels;
using essentialAdmin.Extensions;
using System.Reflection;
using essentialAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using essentialAdmin.Models.EmployeeViewModels;
using System.Collections.Generic;
using essentialAdmin.Data;
using Microsoft.AspNetCore.Identity;

namespace essentialAdmin.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]

    public class Employee : BaseController
    {
        private IEmployeeService _eService;
        private readonly UserManager<ApplicationUser> _userManager;

        public Employee(
            UserManager<ApplicationUser> userManager, IEmployeeService eService)
        {
            _eService = eService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            EmployeeCreateViewModel newEmployee = new EmployeeCreateViewModel();
            newEmployee.EmployeeRoles = _eService.getAvailableRoles();
            return View(newEmployee);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Create(EmployeeCreateViewModel newCustomer)
        {
            if (ModelState.IsValid)
            {
                if (_eService.isUsernameUnique(newCustomer.Email))
                {
                    var user = new ApplicationUser { UserName = newCustomer.Email, Email = newCustomer.Email, FirstName = newCustomer.FirstName, LastName = newCustomer.LastName, EmailConfirmed = true };
                    var result = await _userManager.CreateAsync(user, newCustomer.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, newCustomer.Role);

                        this.AddNotification("Mitarbeiter wurde erstellt", NotificationType.SUCCESS);

                        return this.RedirectToAction("Edit", new { username = newCustomer.Email });
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            this.AddNotification(error.Description, NotificationType.ERROR);
                        }
                    }
                }
                else
                {
                    this.AddNotification("Benutzername wurde bereits verwendet", NotificationType.ERROR);

                }
            }
            else
            {
                this.AddNotification("Benutzer konnte nicht erstellt werden", NotificationType.ERROR);

            }

            newCustomer.EmployeeRoles = _eService.getAvailableRoles();

            // If we got this far, something failed, redisplay form
            return View(newCustomer);
        }

        [HttpGet]
        public IActionResult Edit(string username)
        {
            var model = _eService.loadEmployeeEditModel(username);

            if (model == null)
            {
                this.AddNotification("Konnte Mitarbieter nicht laden", NotificationType.ERROR);

                return this.RedirectToAction("Index");
            }
            model.EmployeeRoles = _eService.getAvailableRoles();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeEditViewModel updatedEmployee)
        {
            if (ModelState.IsValid && updatedEmployee.Role != "Bitte auswählen")
            {
                if (_eService.updateEmployee(updatedEmployee).Result)
                {
                    this.AddNotification("Mitarbeiter wurde aktualisiert", NotificationType.SUCCESS);
                    return this.RedirectToAction("Edit", new { username = updatedEmployee.Email });
                }
            }
            this.AddNotification("Mitarbeiter wurde nicht aktualisiert<br>Überprüfe die Eingaben", NotificationType.WARNING);
            updatedEmployee.EmployeeRoles = _eService.getAvailableRoles();

            return View(updatedEmployee);
        }

        public IActionResult Delete(string username)
        {
            if (_eService.deleteEmployee(username).Result)
            {
                this.AddNotification("Kunde wurde gelöscht", NotificationType.SUCCESS);
            }
            this.AddNotification("Konnte Kunde nicht löschen", NotificationType.ERROR);
            return this.RedirectToAction("Index");
        }

        public IActionResult LoadData()
        {
            return _eService.loadEmployeeDataTable(Request);

        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }

        #region Helper


        private bool isCustomerEmpty(CustomerInputModel c)
        {
            foreach (PropertyInfo pi in c.GetType().GetProperties())
            {
                if (pi.Name != "Title")
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(c);
                        if (!string.IsNullOrEmpty(value))
                        {
                            return false;
                        }
                    }
                    else if (pi.PropertyType == typeof(int))
                    {
                        int value = (int)pi.GetValue(c);
                        if (value != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        #endregion
    }


}