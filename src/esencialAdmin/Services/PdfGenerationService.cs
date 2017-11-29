using esencialAdmin.Data.Models;
using esencialAdmin.Models.PdfViewModels;
using esencialAdmin.Models.SubscriptionViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
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

        public List<PdfSingleAdressViewModel> getAdressLabelsModel(SubscriptionIndexViewModel filter)
        {
            bool filterPlan = false;
            if (filter.planID == 0)
            {
                filterPlan = true;
            }
            bool filterStatus = false;
            if (filter.statusID == 0)
            {
                filterStatus = true;
            }
            int currentYear = DateTime.UtcNow.Year;

            var customer = (from x in this._context.Subscription
                            where 
                            (x.FkPlanId == filter.planID ) || filterPlan && 
                            (x.FkSubscriptionStatus == filter.statusID) || filterStatus && 
                            ((x.Periodes.Any(c => c.PeriodesGoodies.Any(y => y.Received == false && y.SubPeriodeYear <= currentYear))) || !filter.Goody)
                            select x.FkCustomer
                            ).OrderBy(c => c.FirstName).OrderBy(c => c.LastName);


            List<PdfSingleAdressViewModel> labelList = new List<PdfSingleAdressViewModel>();
            foreach(var cust in customer)
            {
                labelList.Add(PdfSingleAdressViewModel.CreateFromCustomer(cust));
            }
            return labelList;
        }
    }
}
