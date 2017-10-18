using essentialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Models.CustomerViewModels
{
    public class CustomerInputModel
    {
        [DisplayName("Anrede")]
        public string Title { get; set; }

        [DisplayName("Vorname")]
        public string FirstName { get; set; }

        [DisplayName("Nachname")]
        public string LastName { get; set; }

        [DisplayName("Strasse")]
        public string Street { get; set; }

        [DisplayName("PLZ")]
        public string Zip { get; set; }

        [DisplayName("Ort")]
        public string City { get; set; }

        [DisplayName("Firma")]
        public string Company { get; set; }

        [DisplayName("Telefonnummer")]
        public string Phone { get; set; }

        [DisplayName("E-Mail Adresse")]
        public string Email { get; set; }

        [DisplayName("Einkäufe")]
        public string PurchasesRemarks { get; set; }

        [DisplayName("Allgemeine")]
        public string GeneralRemarks { get; set; }

        public static CustomerInputModel CreateFromCustomer(Customers c)
        {
            return new CustomerInputModel()
            {
                Title = c.Title,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Street = c.Street,
                Zip = c.Zip,
                City = c.City,
                Company = c.Company,
                Phone = c.Phone,
                PurchasesRemarks = c.PurchasesRemarks
                
            };
        }
    }
}
