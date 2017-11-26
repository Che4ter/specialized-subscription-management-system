using esencialAdmin.Data.Models;
using esencialAdmin.Models.GoodiesViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPlanFilterViewModel
    {
        [DisplayName("Patenschaften")]
        [Required]
        public Dictionary<int, String> Plans { get; set; }

    }
}
