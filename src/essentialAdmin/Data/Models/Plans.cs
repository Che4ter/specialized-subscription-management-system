using System;
using System.Collections.Generic;

namespace essentialAdmin.Data.Models
{
    public partial class Plans : ITrackableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }
        public DateTime? Deadline { get; set; }
        public int? FkTemplateLabel { get; set; }
        public Templates FkTemplateLabelNavigation { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
    }
}
