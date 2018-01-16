using esencialAdmin.Data.Models;
using System;

namespace esencialAdmin.Models.PdfViewModels
{
    public class PdfSingleAdressViewModel
    {
        public String Title { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Street { get; set; }
        public String Zip { get; set; }
        public String City { get; set; }
        public String Company { get; set; }

        public static PdfSingleAdressViewModel CreateFromCustomer(Customers c)
        {
            var newModel = new PdfSingleAdressViewModel()
            {
                Title = c.Title,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Street = c.Street,
                Zip = c.Zip,
                City = c.City,
                Company = c.Company,
            };
            return newModel;
        }
    }
}