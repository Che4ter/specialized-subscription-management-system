using esencialAdmin.Data.Models;
using esencialAdmin.Models.PdfViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace esencialAdmin.Services
{
    public class PdfGenerationService : IPdfGenerationService
    {
        protected readonly esencialAdminContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public PdfGenerationService(esencialAdminContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }


        public PdfCertificateViewModel getCertificateModel(int customerID)
        {
            var customer = this._context.Customers
                             .Include(c => c.Subscription)
                             .Where(c => c.Id == customerID)
                             .FirstOrDefault();

            PdfCertificateViewModel certModel = new PdfCertificateViewModel();
            certModel.Customer = customer.FirstName + " " + customer.LastName;
            certModel.PlantNumber = 3;
            certModel.LastYear = 2017;
            return certModel;
        }

        public List<PdfSingleAdressViewModel> getAdressLabelsModel()
        {
            var customer = this._context.Customers;

            List<PdfSingleAdressViewModel> labelList = new List<PdfSingleAdressViewModel>();
            foreach(var cust in customer)
            {
                labelList.Add(PdfSingleAdressViewModel.CreateFromCustomer(cust));
            }
            return labelList;
        }
    }
}
