using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class SubscriptionPhotos : ITrackableEntity
    {
        public int Id { get; set; }
        public int FkSubscriptionId { get; set; }
        public int FkFileId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public Files FkFile { get; set; }
        public Subscription FkSubscription { get; set; }
    }
}
