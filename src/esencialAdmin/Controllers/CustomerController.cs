using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Data.Models;
using esencialAdmin.Models.CustomerViewModels;
using esencialAdmin.Extensions;
using System.Reflection;
using esencialAdmin.Services;

namespace esencialAdmin.Controllers
{
    public class CustomerController : BaseController
    {
        private ICustomerService _cService;
        public CustomerController(ICustomerService cService)
        {
            _cService = cService;
        }

        public IActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CustomerInputModel newCustomer)
        {

            if (!isCustomerEmpty(newCustomer) && ModelState.IsValid)
            {
                if (_cService.isEmailUnique(newCustomer.Email))
                {
                    var _id = _cService.createNewCustomer(newCustomer);
                    if (_id > 0)
                    {
                        return this.RedirectToAction("Edit", new { id = _id });
                    }
                }
                else
                {
                    this.AddNotification("Kunde wurde nicht erstellt<br>E-Mail ist nicht eindeutig", NotificationType.WARNING);
                }
            }
            this.AddNotification("Kunde wurde nicht erstellt<br>Überprüfe die Eingaben", NotificationType.ERROR);

            return View(newCustomer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _cService.loadCustomerInputModel(id);

            if (model == null)
            {
                this.AddNotification("Konnte Kunde nicht laden", NotificationType.ERROR);

                return this.RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CustomerInputModel updatedCustomer)
        {

            if (!isCustomerEmpty(updatedCustomer) && ModelState.IsValid)
            {

                if (_cService.isEmailUnique(updatedCustomer.Email, updatedCustomer.ID))
                {

                    if (_cService.updateCustomer(updatedCustomer))
                    {
                        this.AddNotification("Kunde wurde aktualisiert", NotificationType.SUCCESS);
                        return this.RedirectToAction("Edit", updatedCustomer.ID);
                    }
                }
                else
                {
                    this.AddNotification("Kunde wurde nicht aktualisiert<br>E-Mail ist nicht eindeutig", NotificationType.WARNING);
                }
            }
            this.AddNotification("Kunde wurde nicht aktualisiert<br>Überprüfe die Eingaben", NotificationType.WARNING);

            return View(updatedCustomer);
        }

        public IActionResult Delete(int id)
        {
            if (_cService.deleteCustomer(id))
            {
                this.AddNotification("Kunde wurde gelöscht", NotificationType.SUCCESS);
            }
            this.AddNotification("Konnte Kunde nicht löschen", NotificationType.ERROR);
            return this.RedirectToAction("Index");
        }

        public IActionResult LoadData()
        {
            return _cService.loadCustomerDataTable(Request);

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