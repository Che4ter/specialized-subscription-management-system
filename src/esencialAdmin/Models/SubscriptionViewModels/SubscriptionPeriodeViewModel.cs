using esencialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPeriodeViewModel
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Display(Name = "Bezahlt")]
        public bool Payed { get; set; }
        public DateTime? PayedDate { get; set; }

        [Display(Name = "Zahlungsmethode")]
        public int? PaymentMethodID { get; set; }

        [Display(Name = "Zahlugngssart")]
        public virtual List<PaymentMethodsViewModel> PaymentMethods { get; set; }

        [Display(Name = "Betrag")]
        public Decimal Price { get; set; }

        [DisplayName("Geschenkt von")]
        public SubscriptionCustomerViewModel GiftetBy { get; set; }

        [Display(Name = "Geschenk")]
        public virtual List<SubscriptionPeriodesGoodiesViewModel> Goodies { get; set; }

        public static SubscriptionPeriodeViewModel CreateFromPeriode(Periodes p)
        {
            var newModel = new SubscriptionPeriodeViewModel()
            {
               ID = p.Id,
               StartDate = p.StartDate,
               EndDate = p.EndDate,
               Payed = p.Payed,
               PayedDate = p.PayedDate,
               PaymentMethodID = p.FkPayedMethodId,
               Price = p.Price,

            };

            if(p.FkGiftedBy != null)
            {
                newModel.GiftetBy = SubscriptionCustomerViewModel.CreateFromCustomer(p.FkGiftedBy);
            }


            return newModel;
        }
    }
}
