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
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;

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
            SubscriptionCreateViewModel newSubscription = new SubscriptionCreateViewModel
            {
                StartDate = DateTime.Now,
                PaymentMethods = _sService.getAvailablePaymentMethods(),
            };

            return View(newSubscription);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(SubscriptionCreateViewModel newSubscription)
        {
            
            if (!iNewesSubscriptionEmpty(newSubscription) && ModelState.IsValid)
            {
                if(_sService.checkIfNrExists(newSubscription.PlantNumber))
                {
                    newSubscription.CustomerPreSelect = _sService.getCustomerSelect2Text(newSubscription.CustomerID);
                    newSubscription.PlanPreSelect = _sService.getPlanSelect2Text(newSubscription.PlanID);
                    newSubscription.PaymentMethods = _sService.getAvailablePaymentMethods();
                    if (newSubscription.GiverCustomerId > 0)
                    {
                        newSubscription.GiverPreSelect = _sService.getCustomerSelect2Text(newSubscription.GiverCustomerId);
                    }
                    this.AddNotification("Rebstock Nummer existiert bereits", NotificationType.ERROR);
                    return View(newSubscription);
                }
                var _id = _sService.createNewSubscription(newSubscription);
                if (_id > 0)
                {
                    return this.RedirectToAction("Edit", new { id = _id });
                }
            }
            newSubscription.PaymentMethods = _sService.getAvailablePaymentMethods();
            newSubscription.CustomerPreSelect = _sService.getCustomerSelect2Text(newSubscription.CustomerID);
            if(newSubscription.GiverCustomerId > 0)
            {
                newSubscription.GiverPreSelect = _sService.getCustomerSelect2Text(newSubscription.GiverCustomerId);
            }
            newSubscription.PlanPreSelect = _sService.getPlanSelect2Text(newSubscription.PlanID);
            this.AddNotification("Patenschaft wurde nicht erstellt<br>Überprüfe die Eingaben", NotificationType.ERROR);
            return View(newSubscription);
        }

        [HttpPost]
        public IActionResult updatePayedStatus(int periodID, bool paymentState)
        {
            if (_sService.updatePaymentStatus(periodID, paymentState))
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        public IActionResult updatePaymentReminderStatus(int periodID, bool reminderState)
        {
            if (_sService.updatePaymentReminderSent(periodID, reminderState))
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        public IActionResult updateReceivedGoodie(int goodyID, bool received)
        {
            if (_sService.updateReceivedGoody(goodyID, received))
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        public async Task<IActionResult> uploadImageForUser(List<IFormFile> files, int subscriptionID)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0 && (formFile.ContentType == "image/jpeg" || formFile.ContentType == "image/jpg" || formFile.ContentType == "image/gif" || formFile.ContentType == "image/png"))
                {
                    await _sService.addSubscriptionPhoto(formFile, subscriptionID);

                }
            }

            return this.RedirectToAction("Edit", new { id = subscriptionID });
        }


        [HttpPost]
        public IActionResult updatePaymentMethod(int periodID, int paymentMethodID)
        {
            if (_sService.updatePaymentMethod(periodID, paymentMethodID))
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        public int getNextFreePlantNr(int planID)
        {
            return _sService.getNextPlantNr(planID);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _sService.loadSubscriptionInputModel(id);

            if (model == null)
            {
                this.AddNotification("Konnte Patenschaft nicht laden", NotificationType.ERROR);

                return this.RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult renewSubscription(int subscriptionID)
        {
            if (_sService.renewSubscription(subscriptionID))
            {
                this.AddNotification("Patenschaft wurde verlängert", NotificationType.SUCCESS);
            }
            else
            {
                this.AddNotification("Patenschaft konnte nicht verlängert werden", NotificationType.ERROR);
            }
            return this.RedirectToAction("Edit", new { id = subscriptionID });
        }

        public IActionResult Delete(int id)
        {
            if (_sService.deleteSubscription(id))
            {
                this.AddNotification("Patenschaft wurde gelöscht", NotificationType.SUCCESS);
            }
            this.AddNotification("Konnte Patenschaft nicht löschen", NotificationType.ERROR);
            return this.RedirectToAction("Index");
        }

        public IActionResult LoadDefaultData()
        {
            return _sService.loadDefaultSubscriptionDataTable(Request);
        }

        public IActionResult LoadGoodiesData()
        {
            return _sService.loadGoodiesSubscriptionDataTable(Request);
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

        public IActionResult managegoodies()
        {
            return View();
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