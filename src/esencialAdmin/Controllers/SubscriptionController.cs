using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Data.Models;
using esencialAdmin.Extensions;
using System.Reflection;
using esencialAdmin.Services;
using esencialAdmin.Models.PlanViewModels;
using esencialAdmin.Models.SubscriptionViewModels;
using System.Collections.Generic;

namespace esencialAdmin.Controllers
{
    public class SubscriptionController : BaseController
    {
        private ISubscriptionService _sService;
        public SubscriptionController(ISubscriptionService sService)
        {
            _sService = sService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            SubscriptionCreateViewModel newSubscription = new SubscriptionCreateViewModel();
            newSubscription.StartDate = DateTime.Now;
            newSubscription.PaymentMethods = _sService.getAvailablePaymentMethods();

            return View(newSubscription);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(SubscriptionCreateViewModel newSubscription)
        {

            if (!iNewesSubscriptionEmpty(newSubscription) && ModelState.IsValid)
            {
                var _id = _sService.createNewSubscription(newSubscription);
                if (_id > 0)
                {
                    return this.RedirectToAction("Edit", new { id = _id });
                }
            }
            this.AddNotification("Patenschaft wurde nicht erstellt<br>Überprüfe die Eingaben", NotificationType.ERROR);
            newSubscription.PaymentMethods = _sService.getAvailablePaymentMethods();
            return View(newSubscription);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _sService.loadPlanInputModel(id);

            if (model == null)
            {
                this.AddNotification("Konnte Plan nicht laden", NotificationType.ERROR);

                return this.RedirectToAction("Index");
            }
            model.Goodies = _sService.getAvailableGoodies();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PlanInputViewModel updatedPlan)
        {

            //if (!iNewesSubscriptionEmpty(updatedPlan) && ModelState.IsValid)
            //{

            //    if (_sService.updatePlan(updatedPlan))
            //    {
            //        this.AddNotification("Plan wurde aktualisiert", NotificationType.SUCCESS);
            //        return this.RedirectToAction("Edit", updatedPlan.ID);
            //    }
            //}
            this.AddNotification("Plan wurde nicht aktualisiert<br>Überprüfe die Eingaben", NotificationType.WARNING);
            updatedPlan.Goodies = _sService.getAvailableGoodies();
            return View(updatedPlan);
        }

        public IActionResult Delete(int id)
        {
            if (_sService.deletePlan(id))
            {
                this.AddNotification("Plan wurde gelöscht", NotificationType.SUCCESS);
            }
            this.AddNotification("Konnte Plan nicht löschen", NotificationType.ERROR);
            return this.RedirectToAction("Index");
        }

        public IActionResult LoadData()
        {
            return _sService.loadPlanDataTable(Request);
        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }


        [HttpGet]
        public IActionResult GetCustomers(string search, int page, int pageSize)
        {
            //Get the paged results and the total count of the results for this query. 
            return _sService.getSelect2Customers(search, pageSize, page);

        }

        [HttpGet]
        public IActionResult GetPlans(string search, int page, int pageSize)
        {
            //Get the paged results and the total count of the results for this query. 
            return _sService.getSelect2Plans(search, pageSize, page);

        }

        #region Helper

        private bool iNewesSubscriptionEmpty(SubscriptionCreateViewModel c)
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