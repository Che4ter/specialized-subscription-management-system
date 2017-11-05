using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Files : ITrackableEntity
    {
        public Files()
        {
            SubscriptionPhotos = new HashSet<SubscriptionPhotos>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string OriginalName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public ICollection<SubscriptionPhotos> SubscriptionPhotos { get; set; }
    }
}
