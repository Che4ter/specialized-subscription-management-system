using System;
using System.Collections.Generic;

namespace esencialAdmin.Data.Models
{
    public partial class Customers : ITrackableEntity
    {
        public Customers()
        {
            Periodes = new HashSet<Periodes>();
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PurchasesRemarks { get; set; }
        public string GeneralRemarks { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public ICollection<Periodes> Periodes { get; set; }
        public ICollection<Subscription> Subscription { get; set; }
    }
}
