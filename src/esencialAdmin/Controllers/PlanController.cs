using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Data.Models;
using esencialAdmin.Extensions;
using System.Reflection;
using esencialAdmin.Services;
using esencialAdmin.Models.PlanViewModels;

namespace esencialAdmin.Controllers
{
    public class PlanController : BaseController
    {
        private IPlanService _pService;
        public PlanController(IPlanService pService)
        {
            _pService = pService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            PlanInputViewModel newPlan = new PlanInputViewModel();
            newPlan.Goodies = _pService.getAvailableGoodies();
            return View(newPlan);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(PlanInputViewModel newPlan)
        {

            if (!isPlanEmpty(newPlan) && ModelState.IsValid)
            {
                var _id = _pService.createNewPlan(newPlan);
                if (_id > 0)
                {
                    return this.RedirectToAction("Edit", new { id = _id });
                }
            }
            this.AddNotification("Plan wurde nicht erstellt<br>Überprüfe die Eingaben", NotificationType.ERROR);
            newPlan.Goodies = _pService.getAvailableGoodies();
            return View(newPlan);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _pService.loadPlanInputModel(id);

            if (model == null)
            {
                this.AddNotification("Konnte Plan nicht laden", NotificationType.ERROR);

                return this.RedirectToAction("Index");
            }
            model.Goodies = _pService.getAvailableGoodies();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PlanInputViewModel updatedPlan)
        {

            if (!isPlanEmpty(updatedPlan) && ModelState.IsValid)
            {

                if (_pService.updatePlan(updatedPlan))
                {
                    this.AddNotification("Plan wurde aktualisiert", NotificationType.SUCCESS);
                    return this.RedirectToAction("Edit", updatedPlan.ID);
                }
            }
            this.AddNotification("Plan wurde nicht aktualisiert<br>Überprüfe die Eingaben", NotificationType.WARNING);
            updatedPlan.Goodies = _pService.getAvailableGoodies();
            return View(updatedPlan);
        }

        public IActionResult Delete(int id)
        {
            if (_pService.deletePlan(id))
            {
                this.AddNotification("Plan wurde gelöscht", NotificationType.SUCCESS);
            }
            this.AddNotification("Konnte Plan nicht löschen", NotificationType.ERROR);
            return this.RedirectToAction("Index");
        }

        public IActionResult LoadData()
        {
            return _pService.loadPlanDataTable(Request);
        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }

        #region Helper

        private bool isPlanEmpty(PlanInputViewModel c)
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