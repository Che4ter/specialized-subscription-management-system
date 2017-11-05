using esencialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPlanViewModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Preis")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Dauer(in Jahren)")]
        public int Duration { get; set; }

       
        public static SubscriptionPlanViewModel CreateFromPlan(Plans p)
        {
            var newModel = new SubscriptionPlanViewModel()
            {
                ID = p.Id,
                Name = p.Name,
                Price = p.Price,
                Duration = p.Duration,           
            };

      
            return newModel;
        }
    }
}
