using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Subscription : ITrackableEntity
    {
        public Subscription()
        {
            SubscriptionPhotos = new HashSet<SubscriptionPhotos>();
        }

        public int Id { get; set; }
        public int FkSubscriptionStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public SubscriptionStatus FkSubscriptionStatusNavigation { get; set; }
        public ICollection<SubscriptionPhotos> SubscriptionPhotos { get; set; }
    }
}
