using esencialAdmin.Data.Models;
using System.ComponentModel;

namespace esencialAdmin.Models.CustomerViewModels
{
    public class CustomerInputModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

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

        [DisplayName("Erstellt am")]
        public string DateCreated { get; set; }

        [DisplayName("Erstellt durch")]
        public string UserCreated { get; set; }

        [DisplayName("Zuletzt bearbeitet am")]
        public string DateModified { get; set; }

        [DisplayName("Zuletzt bearbeitet durch")]
        public string UserModified { get; set; }

        public static CustomerInputModel CreateFromCustomer(Customers c)
        {
            var newModel = new CustomerInputModel()
            {
                ID = c.Id,
                Title = c.Title,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Street = c.Street,
                Zip = c.Zip,
                City = c.City,
                Company = c.Company,
                Phone = c.Phone,
                Email = c.Email,
                PurchasesRemarks = c.PurchasesRemarks,
                GeneralRemarks = c.GeneralRemarks,
                UserCreated = c.UserCreated,
                UserModified = c.UserModified               
            };

            if(c.DateCreated != null)
            {
                newModel.DateCreated = c.DateCreated.Value.ToLocalTime().ToString();
            }
            if (c.DateModified != null)
            {
                newModel.DateModified = c.DateModified.Value.ToLocalTime().ToString();
            }
            return newModel;
        }
    }
}