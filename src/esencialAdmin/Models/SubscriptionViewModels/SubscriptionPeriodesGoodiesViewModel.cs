using esencialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPeriodesGoodiesViewModel
    {
        public int ID { get; set; }
        public bool Received { get; set; }
        public DateTime? ReceivedAt { get; set; }
        
        public int SubPeriodeYear { get; set; }
       

        public static SubscriptionPeriodesGoodiesViewModel CreateFromGoodie(SubscriptionPeriodesGoodiesViewModel g)
        {
            var newModel = new SubscriptionPeriodesGoodiesViewModel()
            {
               ID = g.ID,
               Received = g.Received,
               ReceivedAt = g.ReceivedAt,
               SubPeriodeYear = g.SubPeriodeYear,           
            };

            return newModel;
        }
    }
}
