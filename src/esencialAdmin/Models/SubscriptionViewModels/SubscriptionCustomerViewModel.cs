using esencialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionCustomerViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Company { get; set; }

        [DisplayName("E-Mail Adresse:")]
        public string Email { get; set; }

        [DisplayName("Telefon Nr:")]
        public string Phone { get; set; }

        public static SubscriptionCustomerViewModel CreateFromCustomer(Customers c)
        {
            var newModel = new SubscriptionCustomerViewModel()
            {
                Title = c.Title,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Street = c.Street,
                Zip = c.Zip,
                City = c.City,
                Company = c.Company,
                Email = c.Email,
                Phone = c.Phone,
                ID = c.Id
            };

            return newModel;
        }
    }
}
