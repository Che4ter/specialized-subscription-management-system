using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPlanFilterViewModel
    {
        [DisplayName("Patenschaften")]
        [Required]
        public Dictionary<int, String> Plans { get; set; }
    }
}