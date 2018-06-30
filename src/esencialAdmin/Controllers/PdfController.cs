using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Services;
using Rotativa.NetCore;
using esencialAdmin.Models.SubscriptionViewModels;
using Rotativa.NetCore.Options;
using System.Collections.Generic;

//https://github.com/Stefanone91/Rotativa.NetCore

namespace esencialAdmin.Controllers
{
    public class Pdf : BaseController
    {
        private IPdfGenerationService _pService;
        public Pdf(IPdfGenerationService pService)
        {
            _pService = pService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrintCertificate(int Id, int TemplateID)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies)
            {
                cookieCollection.Add(key.Key, key.Value);
            }
            ActionAsPdf pdf;

            if (TemplateID == 1)
            {
                pdf = new ActionAsPdf("GenerateCertificateWine", new { id = Id });
            }
            else
            {
                pdf = new ActionAsPdf("GenerateCertificateOlive", new { id = Id });
            }
            pdf.FileName = "Zertifikat.pdf";
            pdf.Cookies = cookieCollection;
            return pdf;
        }

        [HttpGet]
        public IActionResult GenerateCertificateOlive(int id)
        {
            return View(_pService.getCertificateModel(id));
        }

        [HttpGet]
        public IActionResult GenerateCertificateWine(int id)
        {
            return View(_pService.getCertificateModel(id));
        }

        [HttpPost]
        public IActionResult PrintAdressLabels(SubscriptionIndexViewModel filter)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies)
            {
                cookieCollection.Add(key.Key, key.Value);
            }

            var pdf = new ActionAsPdf("GenerateAdressLabels", filter);
            pdf.FileName = "AdressEtiketten.pdf";
            pdf.PageMargins = new Margins(0, 0, 0, 0);
            pdf.Cookies = cookieCollection;
            return pdf;
        }

        [HttpPost]
        public IActionResult PrintPictureTemplate(SubscriptionIndexViewModel filter)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies)
            {
                cookieCollection.Add(key.Key, key.Value);
            }

            var pdf = new ActionAsPdf("GeneratePictureTemplate", filter);
            pdf.FileName = "A5.pdf";
            pdf.PageSize = Size.A4;
            pdf.PageOrientation = Orientation.Portrait;
            pdf.PageMargins = new Margins(0, 0, 0, 0);
            pdf.Cookies = cookieCollection;
            return pdf;
        }

        [HttpPost]
        public IActionResult PrintBottleLabels(SubscriptionIndexViewModel filter)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies)
            {
                cookieCollection.Add(key.Key, key.Value);
            }

            var pdf = new ActionAsPdf("GenerateBottleLabels", filter);
            pdf.FileName = "Etiketten.pdf";
            pdf.PageMargins = new Margins(0,0,0,0);
            pdf.Cookies = cookieCollection;
            return pdf;
        }

        [HttpGet]
        public IActionResult GenerateAdressLabels(SubscriptionIndexViewModel filter)
        {
            return View(_pService.getAdressLabelsModel(filter));
        }

        [HttpGet]
        public IActionResult GeneratePictureTemplate(SubscriptionIndexViewModel filter)
        {
            return View(_pService.getPictureTemplatesModel(filter));
        }

        [HttpGet]

        public IActionResult GenerateBottleLabels(SubscriptionIndexViewModel filter)
        {
            return View(_pService.getBottleLabels(filter));
        }
    }
}