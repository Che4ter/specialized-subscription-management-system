using essentialAdmin.Data.Models;
using essentialAdmin.Models.CustomerViewModels;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace essentialAdmin.Services
{
    public class CustomerService : ICustomerService
    {
        protected readonly essentialAdminContext _context;

        public CustomerService(essentialAdminContext context)
        {
            _context = context;
        }

        public int createNewCustomer(CustomerInputModel newCustomer)
        {
            try
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
                return c.Id;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public bool deleteCustomer(int id)
        {
            try
            {
                this._context.Customers.Remove(this._context.Customers.Where(r => r.Id == id).FirstOrDefault());
                this._context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JsonResult loadCustomerDataTable(HttpRequest Request)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
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
                return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CustomerInputModel loadCustomerInputModel(int id)
        {
            var customerToLoad = this._context.Customers
                  .Where(c => c.Id == id)
                  .FirstOrDefault();

            if (customerToLoad == null)
            {
                return null;
            }

            return CustomerInputModel.CreateFromCustomer(customerToLoad);
        }

        public bool updateCustomer(CustomerInputModel customerToUpdate)
        {
            try
            {
                var customerToEdit = this._context.Customers
                  .Where(c => c.Id == customerToUpdate.ID)
                  .FirstOrDefault();
                if (customerToEdit == null)
                {
                    return false;
                }

                customerToEdit.Title = customerToUpdate.Title;
                customerToEdit.FirstName = customerToUpdate.FirstName;
                customerToEdit.LastName = customerToUpdate.LastName;
                customerToEdit.Street = customerToUpdate.Street;
                customerToEdit.Zip = customerToUpdate.Zip;
                customerToEdit.City = customerToUpdate.City;
                customerToEdit.Company = customerToUpdate.Company;
                customerToEdit.Phone = customerToUpdate.Phone;
                customerToEdit.PurchasesRemarks = customerToUpdate.PurchasesRemarks;
                customerToEdit.GeneralRemarks = customerToUpdate.GeneralRemarks;

                this._context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
