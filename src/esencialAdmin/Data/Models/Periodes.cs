using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Periodes : ITrackableEntity
    {
        public Periodes()
        {
            PeriodesGoodies = new HashSet<PeriodesGoodies>();
        }

        public int Id { get; set; }
        public int FkSubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Payed { get; set; }
        public DateTime? PayedDate { get; set; }
        public int? FkPayedMethodId { get; set; }
        public decimal Price { get; set; }
        public int? FkGiftedById { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public Customers FkGiftedBy { get; set; }
        public PaymentMethods FkPayedMethod { get; set; }
        public ICollection<PeriodesGoodies> PeriodesGoodies { get; set; }
    }
}
