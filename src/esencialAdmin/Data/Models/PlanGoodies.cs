using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class PlanGoodies
    {
        public PlanGoodies()
        {
            PeriodesGoodies = new HashSet<PeriodesGoodies>();
            Plans = new HashSet<Plans>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? FkTemplateLabel { get; set; }

        public Templates FkTemplateLabelNavigation { get; set; }
        public ICollection<PeriodesGoodies> PeriodesGoodies { get; set; }
        public ICollection<Plans> Plans { get; set; }
    }
}
