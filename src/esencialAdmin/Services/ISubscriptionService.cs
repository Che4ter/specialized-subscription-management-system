﻿using esencialAdmin.Models.GoodiesViewModels;
using esencialAdmin.Models.PlanViewModels;
using esencialAdmin.Models.SubscriptionViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public interface ISubscriptionService
    {
        int createNewSubscription(SubscriptionCreateViewModel newPlan);
        bool deletePlan(int id);
        JsonResult loadPlanDataTable(HttpRequest Request);
        PlanInputViewModel loadPlanInputModel(int id);
        bool updatePlan(PlanInputViewModel planToUpdate);

        List<GoodiesViewModel> getAvailableGoodies();

        JsonResult getSelect2Customers(string searchTerm, int pageSize, int pageNum);

        JsonResult getSelect2Plans(string searchTerm, int pageSize, int pageNum);

        List<PaymentMethodsViewModel> getAvailablePaymentMethods();

    }
}