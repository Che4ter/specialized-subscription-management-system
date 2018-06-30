using esencialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionPeriodeViewModel
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool CurrentPeriode { get; set; }

        [Display(Name = "Bezahlt")]
        public bool Payed { get; set; }

        [Display(Name = "Zahlungserinnerung gesendet")]
        public bool PaymentReminderSent { get; set; }

        //[Display(Name = "Zahlungserinnerung gesendet am")]
        //public DateTime? PaymentReminderSentDate { get; set; }

        [Display(Name = "Zahlungsmethode")]
        public int? PaymentMethodID { get; set; }

        [Display(Name = "Zahlungsart")]
        public virtual List<PaymentMethodsViewModel> PaymentMethods { get; set; }

        [Display(Name = "Betrag")]
        public Decimal Price { get; set; }

        [DisplayName("Geschenkt von")]
        public SubscriptionCustomerViewModel GiftetBy { get; set; }

        [Display(Name = "Ernteanteil")]
        public virtual List<SubscriptionPeriodesGoodiesViewModel> Goodies { get; set; }

        public string GoodiesLabel { get; set; }

        public static SubscriptionPeriodeViewModel CreateFromPeriode(Periodes p)
        {
            var newModel = new SubscriptionPeriodeViewModel()
            {
                ID = p.Id,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Payed = p.Payed,
                PaymentMethodID = p.FkPayedMethodId,
                Price = p.Price,
                CurrentPeriode = false,
                PaymentReminderSent = p.PaymentReminderSent,
            };

            if (p.FkGiftedBy != null)
            {
                newModel.GiftetBy = SubscriptionCustomerViewModel.CreateFromCustomer(p.FkGiftedBy);
            }

            newModel.Goodies = new List<SubscriptionPeriodesGoodiesViewModel>();

            return newModel;
        }
    }
}