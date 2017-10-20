using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using essentialAdmin.Data.Models;
using essentialAdmin.Models.CustomerViewModels;
using essentialAdmin.Extensions;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Linq.Expressions;

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

        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name                  
                var columnIndex = Request.Form["order[0][column]"].ToString();

               // var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumn = Request.Form[$"columns[{columnIndex}][data]"].ToString();

                var sortDirection = Request.Form["order[0][dir]"].ToString();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var customerData = (from tempcustomer in _context.Customers
                                    select tempcustomer);

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    sortColumn = sortColumn.Substring(0, 1).ToUpper() + sortColumn.Remove(0, 1); 
                    customerData = customerData.OrderBy(sortColumn + ' ' + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName == searchValue);
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                String a = ex.Message;
                throw;
            }

        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
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