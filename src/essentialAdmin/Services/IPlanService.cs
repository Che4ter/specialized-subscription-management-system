using essentialAdmin.Models.PlanViewModels;
using essentialAdmin.Models.TemplateViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace essentialAdmin.Services
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