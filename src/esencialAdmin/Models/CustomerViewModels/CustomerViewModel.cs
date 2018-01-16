using System.ComponentModel;

namespace esencialAdmin.Models.CustomerViewModels
{
    public class CustomerViewModel
    {
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

        [DisplayName("Erstellt am")]
        public string DateCreated { get; set; }

        [DisplayName("Erstellt durch")]
        public string UserCreated { get; set; }

        [DisplayName("Zuletzt bearbeitet am")]
        public string DateModified { get; set; }

        [DisplayName("Zuletzt bearbeitet durch")]
        public string UserModified { get; set; }
    }
}