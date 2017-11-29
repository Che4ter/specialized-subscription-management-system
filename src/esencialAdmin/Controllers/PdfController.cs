using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Services;
using Rotativa.NetCore;
using esencialAdmin.Models.PdfViewModels;
using Microsoft.AspNetCore.Authorization;
using esencialAdmin.Models.SubscriptionViewModels;

//https://github.com/Stefanone91/Rotativa.NetCore

namespace esencialAdmin.Controllers
{
    public class Pdf : Controller
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

        [Authorize(Policy = "RequireEmployeeRole")]
        public IActionResult PrintCertificate(int id)
        {
            return new ActionAsPdf("GenerateCertificate", new { id = id }) { FileName = "Test.pdf" };
        }

        [HttpGet]
        public IActionResult GenerateCertificate(int id)
        {
            return View(_pService.getCertificateModel(id));
        }

        [HttpPost]
        [Authorize(Policy = "RequireEmployeeRole")]
        public IActionResult PrintAdressLabels(SubscriptionIndexViewModel filter)
        {
            return new ActionAsPdf("GenerateAdressLabels", filter) { FileName = "AdressEtiketten.pdf" };
        }

        [HttpPost]
        [Authorize(Policy = "RequireEmployeeRole")]
        public IActionResult PrintBottleLabels(SubscriptionIndexViewModel filter)
        {
            return new ActionAsPdf("GenerateAdressLabels", filter) { FileName = "AdressEtiketten.pdf" };
        }


        [HttpGet]
        public IActionResult GenerateAdressLabels(SubscriptionIndexViewModel filter)
        {
            return View(_pService.getAdressLabelsModel(filter));
        }

        #region Helper

        #endregion
    }
}