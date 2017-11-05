using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Templates
    {
        public Templates()
        {
            PlanGoodies = new HashSet<PlanGoodies>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PlanGoodies> PlanGoodies { get; set; }
    }
}
