using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using essentialAdmin.Data.Models;
using essentialAdmin.Models.CustomerViewModels;
using essentialAdmin.Extensions;
using System.Reflection;

namespace essentialAdmin.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(essentialAdminContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            var customers = this._context.Customers
                .OrderBy(e => e.LastName)
                .Select(e => new CustomerViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    Street = e.Street,
                    Zip = e.Zip,
                    City = e.City
                });
            return View(customers);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            this.AddNotification("test",NotificationType.SUCCESS);
                return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CustomerInputModel newCustomer)
        {
            if (!isCustomerEmpty(newCustomer) && ModelState.IsValid)
            {
                var c = new Customers()
                {
                    Title = newCustomer.Title,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    Street = newCustomer.Street,
                    Zip = newCustomer.Zip,
                    City = newCustomer.City,
                    Company = newCustomer.Company,
                    Phone = newCustomer.Phone,
                    PurchasesRemarks = newCustomer.PurchasesRemarks
                };
                this._context.Customers.Add(c);
                this._context.SaveChanges();
                return this.RedirectToAction("Edit", c.Id);                
            }
                return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customerToEdit = this.LoadCustomer(id);
            if(customerToEdit == null)
            {
                return this.RedirectToAction("Index");
            }
            var model = CustomerInputModel.CreateFromCustomer(customerToEdit);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CustomerInputModel updatedCustomer)
        {
            var customerToEdit = this.LoadCustomer(id);
            if (customerToEdit == null)
            {
                return this.RedirectToAction("Index");
            }
            if (updatedCustomer != null && ModelState.IsValid)
            {
                customerToEdit.Title = updatedCustomer.Title;
                customerToEdit.FirstName = updatedCustomer.FirstName;
                customerToEdit.LastName = updatedCustomer.LastName;
                customerToEdit.Street = updatedCustomer.Street;
                customerToEdit.Zip = updatedCustomer.Zip;
                customerToEdit.City = updatedCustomer.City;
                customerToEdit.Company = updatedCustomer.Company;
                customerToEdit.Phone = updatedCustomer.Phone;
                customerToEdit.PurchasesRemarks = updatedCustomer.PurchasesRemarks;
                customerToEdit.GeneralRemarks = updatedCustomer.GeneralRemarks;

                this._context.SaveChanges();
                return this.RedirectToAction("Edit", customerToEdit.Id);
            }
            return View();
        }

        #region Helper
        private Customers LoadCustomer(int id)
        {
            var customerToEdit = this._context.Customers
                   .Where(c => c.Id == id)
                   .FirstOrDefault();
            return customerToEdit;
        }

        private bool isCustomerEmpty(CustomerInputModel c)
        {
            foreach (PropertyInfo pi in c.GetType().GetProperties())
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
            return true;
        }

        #endregion
    }
}