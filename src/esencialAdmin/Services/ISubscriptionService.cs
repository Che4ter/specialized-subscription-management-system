using esencialAdmin.Models.GoodiesViewModels;
using esencialAdmin.Models.PlanViewModels;
using esencialAdmin.Models.SubscriptionViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace esencialAdmin.Services
{
    public interface ISubscriptionService
    {
        int createNewSubscription(SubscriptionCreateViewModel newPlan);
        bool deleteSubscription(int id);
        JsonResult loadDefaultSubscriptionDataTable(HttpRequest Request);

        JsonResult loadGoodiesSubscriptionDataTable(HttpRequest Request);
       
        SubscriptionEditViewModel loadSubscriptionInputModel(int id);
        bool updatePlan(PlanInputViewModel planToUpdate);

        List<GoodiesViewModel> getAvailableGoodies();

        JsonResult getSelect2Customers(string searchTerm, int pageSize, int pageNum);

        JsonResult getSelect2Plans(string searchTerm, int pageSize, int pageNum);

        List<PaymentMethodsViewModel> getAvailablePaymentMethods();

        bool updatePaymentStatus(int periodeID, bool isPayed);

        bool updatePaymentMethod(int periodeID, int paymentID);

        bool updateReceivedGoody(int goodyID, bool hasReceived);

        bool updatePaymentReminderSent(int periodeID, bool isReminderSent);

        bool renewSubscription(int subId);

        Task<bool> addSubscriptionPhoto(IFormFile formFile, int subscriptionID);

        void updateSubscriptionStatus();

        SubscriptionPlanFilterViewModel getAvailablePlans();

    }
}