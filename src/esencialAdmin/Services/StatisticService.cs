using esencialAdmin.Data.Models;
using esencialAdmin.Models.HomeViewModels;
using System.Linq;

namespace esencialAdmin.Services
{
    public class StatisticService : IStatisticService
    {
        protected readonly esencialAdminContext _context;

        public StatisticService(esencialAdminContext context)
        {
            _context = context;
        }

        public OverviewHomeViewModel getOverViewModel()
        {
            OverviewHomeViewModel overviewModel = new OverviewHomeViewModel();

            overviewModel.NumberOfCustomers = _context.Customers.Count();
            overviewModel.NumberOfSubscriptions = _context.Subscription.Count();
            overviewModel.NumberOfActiveSubscriptions = _context.Subscription.Where(x => x.FkSubscriptionStatus == 1).Count();
            overviewModel.NumberOfNotPayedSubscriptions = _context.Subscription.Where(x => x.FkSubscriptionStatus == 3).Count();
            overviewModel.NumberOfEndingSubscriptions = _context.Subscription.Where(x => x.FkSubscriptionStatus == 2).Count();

            return overviewModel;
        }
    }
}