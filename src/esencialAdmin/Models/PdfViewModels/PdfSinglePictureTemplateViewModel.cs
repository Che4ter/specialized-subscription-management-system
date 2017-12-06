using esencialAdmin.Data.Models;
using esencialAdmin.Models.GoodiesViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.PdfViewModels
{
    public class PdfSinglePictureTemplateViewModel
    {
        public String Title { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Street { get; set; }
        public String Zip { get; set; }
        public String City { get; set; }
        public String Company { get; set; }

        public static PdfSinglePictureTemplateViewModel CreateFromCustomer(Customers c)
        {
            var newModel = new PdfSinglePictureTemplateViewModel()
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
