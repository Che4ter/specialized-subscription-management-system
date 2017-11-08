using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class PaymentMethodsViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Methode")]
        public string Name { get; set; }
    }
}