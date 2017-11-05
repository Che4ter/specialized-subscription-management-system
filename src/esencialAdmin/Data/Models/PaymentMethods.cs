using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class PaymentMethods
    {
        public PaymentMethods()
        {
            Periodes = new HashSet<Periodes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Fee { get; set; }

        public ICollection<Periodes> Periodes { get; set; }
    }
}
