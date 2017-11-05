using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class PeriodesGoodies : ITrackableEntity
    {
        public int Id { get; set; }
        public int FkPeriodesId { get; set; }
        public int FkPlanGoodiesId { get; set; }
        public bool? Received { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public int SubPeriodeYear { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public Periodes FkPeriodes { get; set; }
        public PlanGoodies FkPlanGoodies { get; set; }
    }
}
