using esencialAdmin.Models.HomeViewModels;

namespace esencialAdmin.Services
{
    public interface IStatisticService
    {
        OverviewHomeViewModel getOverViewModel();
    }
}