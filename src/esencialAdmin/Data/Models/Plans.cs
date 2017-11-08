using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Plans : ITrackableEntity
    {
        public Plans()
        {
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public DateTime Deadline { get; set; }
        public int FkGoodyId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public PlanGoodies FkGoody { get; set; }
        public ICollection<Subscription> Subscription { get; set; }
    }
}
