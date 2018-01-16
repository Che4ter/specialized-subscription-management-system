using esencialAdmin.Data.Models;
using System;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPeriodesGoodiesViewModel
    {
        public int ID { get; set; }
        public bool Received { get; set; }
        public DateTime? ReceivedAt { get; set; }

        public int SubPeriodeYear { get; set; }

        public static SubscriptionPeriodesGoodiesViewModel CreateFromGoodie(PeriodesGoodies g)
        {
            var newModel = new SubscriptionPeriodesGoodiesViewModel()
            {
                ID = g.Id,
                Received = g.Received,
                ReceivedAt = g.ReceivedAt,
                SubPeriodeYear = g.SubPeriodeYear,
            };

            return newModel;
        }
    }
}