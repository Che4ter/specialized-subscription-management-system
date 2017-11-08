using esencialAdmin.Data.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Models.PlanViewModels;
using System.Collections.Generic;
using esencialAdmin.Models.GoodiesViewModels;
using esencialAdmin.Extensions;
using esencialAdmin.Models.SubscriptionViewModels;

namespace esencialAdmin.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        protected readonly esencialAdminContext _context;

        public SubscriptionService(esencialAdminContext context)
        {
            _context = context;
        }


        public int createNewSubscription(SubscriptionCreateViewModel newSubscription)
        {
            try
            {
                var s = new Subscription()
                {
                    FkCustomerId = newSubscription.CustomerID,
                    FkPlanId = newSubscription.PlanID,
                    PlantNumber = newSubscription.PlantNumber,              
                };
                var p = new Periodes()
                {
                    FkSubscriptionId = s.Id,
                    StartDate = newSubscription.StartDate,
                    Payed = newSubscription.Payed

                };
                this._context.Subscription.Add(s);
                this._context.SaveChanges();
                return s.Id;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public bool deletePlan(int id)
        {
            try
            {
                this._context.Plans.Remove(this._context.Plans.Where(r => r.Id == id).FirstOrDefault());
                this._context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<GoodiesViewModel> getAvailableGoodies()
        {
            List<GoodiesViewModel> goodiesList = new List<GoodiesViewModel>();

            foreach (PlanGoodies goody in _context.PlanGoodies)
            {
                goodiesList.Add(new GoodiesViewModel
                {
                    Id = goody.Id,
                    Name = goody.Name
                });
            }

            return goodiesList;
        }

        public List<PaymentMethodsViewModel> getAvailablePaymentMethods()
        {
            List<PaymentMethodsViewModel> paymentList = new List<PaymentMethodsViewModel>();

            foreach (PaymentMethods method in _context.PaymentMethods)
            {
                paymentList.Add(new PaymentMethodsViewModel
                {
                    Id = method.Name,
                    Name = method.Name
                });
            }

            return paymentList;
        }

        public JsonResult getSelect2Customers(string searchTerm, int pageSize, int pageNum)
        {
            try
            {
          
                // Getting all Customer data  
                var customerData = (from tmpcustomer in _context.Customers
                                select new {Id = tmpcustomer.Id, DisplayString = ((tmpcustomer.Company ?? "") + " " + (tmpcustomer.FirstName ?? "") + " " + (tmpcustomer.LastName ?? "") + " " + (tmpcustomer.Street ?? "") + " " + (tmpcustomer.Zip ?? "") + " " + (tmpcustomer.City ?? "")).Replace("  "," ").Trim() });

                //Sorting  
                customerData = customerData.OrderBy(x => x.DisplayString);

                //Search  
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    customerData = customerData.Where(m => m.DisplayString.Contains(searchTerm));
                }

                //total number of rows count   
                var recordsTotal = customerData.Count();
                var skip = pageNum * pageSize;
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();

                var resultList = new List<Select2Result>();
                foreach(var item in data)
                {
                    resultList.Add(new Select2Result()
                    {
                        text = item.DisplayString.Replace("  ", " "),
                        id = item.Id.ToString()
                    });
                }

                //Returning Json Data  
                return new JsonResult(new { items = resultList, total = recordsTotal });

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult getSelect2Plans(string searchTerm, int pageSize, int pageNum)
        {
            try
            {

                // Getting all Plan data  
                var planData = (from tmpplan in _context.Plans
                                    select new { Id = tmpplan.Id, DisplayString = ((tmpplan.Name ?? "") + " - Laufzeit: " + (tmpplan.Duration.ToString() ?? "") + " Monate - Preis " + (tmpplan.Price.ToString("N2") ?? "")).Replace("  ", " ").Trim() });

                //Sorting  
                planData = planData.OrderBy(x => x.DisplayString);

                //Search  
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    planData = planData.Where(m => m.DisplayString.Contains(searchTerm));
                }

                //total number of rows count   
                var recordsTotal = planData.Count();
                var skip = pageNum * pageSize;
                //Paging   
                var data = planData.Skip(skip).Take(pageSize).ToList();

                var resultList = new List<Select2Result>();
                foreach (var item in data)
                {
                    resultList.Add(new Select2Result()
                    {
                        text = item.DisplayString.Replace("  ", " "),
                        id = item.Id.ToString()
                    });
                }

                //Returning Json Data  
                return new JsonResult(new { items = resultList, total = recordsTotal });

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public JsonResult loadPlanDataTable(HttpRequest Request)
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
                var planData = (from tempplan in _context.Plans
                                    select new { Id = tempplan.Id, Name = tempplan.Name, Price = tempplan.Price, Duration = tempplan.Duration, inuse = "notimplemented" });

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    sortColumn = sortColumn.Substring(0, 1).ToUpper() + sortColumn.Remove(0, 1);
                    planData = planData.OrderBy(sortColumn + ' ' + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    planData = planData.Where(m => m.Name.StartsWith(searchValue));
                }

                //total number of rows count   
                recordsTotal = planData.Count();
                //Paging   
                var data = planData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PlanInputViewModel loadPlanInputModel(int id)
        {
            var planToLoad = this._context.Plans
                  .Where(c => c.Id == id)
                  .FirstOrDefault();

            if (planToLoad == null)
            {
                return null;
            }

            return PlanInputViewModel.CreateFromPlan(planToLoad);
        }

        public bool updatePlan(PlanInputViewModel planToUpdate)
        {
            try
            {
                var planToEdit = this._context.Plans
                  .Where(c => c.Id == planToUpdate.ID)
                  .FirstOrDefault();
                if (planToEdit == null)
                {
                    return false;
                }
                planToEdit.Name = planToUpdate.Name;
                planToEdit.Price = planToUpdate.Price;
                planToEdit.Duration = planToUpdate.Duration;
                planToEdit.Deadline = planToUpdate.Deadline;
                planToEdit.FkGoodyId = planToUpdate.GoodyID;

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
