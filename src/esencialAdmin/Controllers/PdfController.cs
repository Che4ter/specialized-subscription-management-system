using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Services;
using Rotativa.NetCore;
using esencialAdmin.Models.PdfViewModels;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Policy = "RequireEmployeeRole")]
        public IActionResult PrintAdressLabels()
        {
            return new ActionAsPdf("GenerateAdressLabels") { FileName = "AdressEtiketten.pdf" };
        }



        [HttpGet]
        public IActionResult GenerateAdressLabels()
        {
            return View(_pService.getAdressLabelsModel());

        }

        #region Helper



        #endregion
    }


}