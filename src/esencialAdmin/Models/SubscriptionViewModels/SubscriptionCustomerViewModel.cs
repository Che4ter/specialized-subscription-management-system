using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionCustomerViewModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
    }
}
