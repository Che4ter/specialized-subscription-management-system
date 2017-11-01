using esencialAdmin.Data.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Models.PlanViewModels;
using esencialAdmin.Models.TemplateViewModels;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public class PlanService : IPlanService
    {
        protected readonly esencialAdminContext _context;

        public PlanService(esencialAdminContext context)
        {
            _context = context;
        }

        public int createNewPlan(PlanInputViewModel newPlan)
        {
            try
            {
                var p = new Plans()
                {
                    Name = newPlan.Name,
                    Price = newPlan.Price,
                    Duration = newPlan.Duration,
                    Deadline = newPlan.Deadline,
                    FkTemplateLabel = newPlan.TemplateID
                };
                this._context.Plans.Add(p);
                this._context.SaveChanges();
                return p.Id;
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

        public List<TemplateViewModel> getAvailableTemplates()
        {
            List<TemplateViewModel> templateList = new List<TemplateViewModel>();

            foreach (Templates template in _context.Templates)
            {
                templateList.Add(new TemplateViewModel
                {
                    Id = template.Id,
                    Name = template.Name
                });
            }

            return templateList;
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
                planToEdit.FkTemplateLabel = planToUpdate.TemplateID;

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
