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

        public PdfCertificateViewModel getCertificateModel(int id)
        {
            var subscription = this._context.Subscription
                             .Include(c => c.FkCustomer)
                             .Include(c => c.Periodes)
                             .Where(c => c.Id == id)
                             .FirstOrDefault();

            PdfCertificateViewModel certModel = new PdfCertificateViewModel();
            certModel.Customer = subscription.FkCustomer.FirstName + " " + subscription.FkCustomer.LastName;
            certModel.PlantNumber = subscription.PlantNumber.Value;
            certModel.LastYear = subscription.Periodes.OrderByDescending(c => c.EndDate).FirstOrDefault().EndDate.Year;
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
                            (x.FkPlanId == filter.planID || filterPlan) &&
                            (x.FkSubscriptionStatus == filter.statusID || filterStatus)
                            select x.FkCustomer
                            ).OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();

            //((x.Periodes.Any(c => c.PeriodesGoodies.Any(y => y.Received == false && y.SubPeriodeYear <= currentYear))) || !filter.Goody)
            List<PdfSingleAdressViewModel> labelList = new List<PdfSingleAdressViewModel>();
            foreach (var cust in customer)
            {
                labelList.Add(PdfSingleAdressViewModel.CreateFromCustomer(cust));
            }
            return labelList;
        }

        public List<PdfSinglePictureTemplateViewModel> getPictureTemplatesModel(SubscriptionIndexViewModel filter)
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
                            (x.FkPlanId == filter.planID || filterPlan) &&
                            (x.FkSubscriptionStatus == filter.statusID || filterStatus) &&
                            (x.SubscriptionPhotos.Count == 0)
                            select new { Firstname = x.FkCustomer.FirstName, Lastname = x.FkCustomer.LastName, PlantNr = x.PlantNumber }
                            ).OrderBy(c => c.PlantNr).ThenBy(c => c.Lastname).ToList();

            //((x.Periodes.Any(c => c.PeriodesGoodies.Any(y => y.Received == false && y.SubPeriodeYear <= currentYear))) || !filter.Goody)
            List<PdfSinglePictureTemplateViewModel> labelList = new List<PdfSinglePictureTemplateViewModel>();
            foreach (var cust in customer)
            {
                PdfSinglePictureTemplateViewModel tmp = new PdfSinglePictureTemplateViewModel();
                tmp.FirstName = cust.Firstname;
                tmp.LastName = cust.Lastname;
                tmp.PlantNr = cust.PlantNr.ToString();
                labelList.Add(tmp);
            }
            return labelList;
        }

        public List<PdfSingleBottleLabelViewModel> getBottleLabels(SubscriptionIndexViewModel filter)
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

            var data = (from x in this._context.Subscription
                        where
                        (x.FkPlanId == filter.planID || filterPlan) &&
                        (x.FkSubscriptionStatus == filter.statusID || filterStatus) &&
                        (x.Periodes.Any(c => c.PeriodesGoodies.Any(y => y.Received == false && y.SubPeriodeYear <= currentYear)))
                        select new { FirstName = x.FkCustomer.FirstName, LastName = x.FkCustomer.LastName, Nr = x.PlantNumber, Bezeichnung = x.FkPlan.FkGoody.Bezeichnung }
                            )
                            .OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();

            List<PdfSingleBottleLabelViewModel> labelList = new List<PdfSingleBottleLabelViewModel>();

            foreach (var item in data)
            {
                labelList.Add(PdfSingleBottleLabelViewModel.Create(item.FirstName + " " + item.LastName, item.Nr.ToString(), item.Bezeichnung));
            }
            return labelList;
        }
    }
}