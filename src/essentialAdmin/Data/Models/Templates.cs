using System;
using System.Collections.Generic;

namespace essentialAdmin.Data.Models
{
    public partial class Templates
    {
        public Templates()
        {
            Plans = new HashSet<Plans>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Plans> Plans { get; set; }
    }
}
