using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionIndexViewModel
    {

        [Display(Name = "Plan")]
        public virtual List<SubscriptionSelectPlanViewModel> Plans { get; set; }

        public int planID { get; set; }

        [Display(Name = "Status")]
        public virtual List<SubscriptionSelectStatusViewModel> Status { get; set; }

        public int statusID { get; set; }
    }
}