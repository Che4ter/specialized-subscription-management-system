using esencialAdmin.Models.PlanViewModels;
using esencialAdmin.Models.TemplateViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public interface IPlanService
    {
        int createNewPlan(PlanInputViewModel newPlan);
        bool deletePlan(int id);
        JsonResult loadPlanDataTable(HttpRequest Request);
        PlanInputViewModel loadPlanInputModel(int id);
        bool updatePlan(PlanInputViewModel planToUpdate);

        List<TemplateViewModel> getAvailableTemplates();
    }
}