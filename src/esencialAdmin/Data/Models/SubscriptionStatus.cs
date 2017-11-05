using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class SubscriptionStatus
    {
        public SubscriptionStatus()
        {
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Label { get; set; }

        public ICollection<Subscription> Subscription { get; set; }
    }
}
