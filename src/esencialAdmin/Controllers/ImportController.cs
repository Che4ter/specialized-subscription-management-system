using esencialAdmin.Extensions;
using esencialAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Controllers
{


    [Authorize(Policy = "RequireAdminRole")]
    public class ImportController : BaseController
    {
        private IImportService _iService;

        public ImportController(IImportService iService)
        {
            _iService = iService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportCustomer(IFormFile file)
        {
            int count = _iService.importCustomer(file);
            if (count >= 0)
            {
                this.AddNotification(count + " Kunden wurden importiert", NotificationType.SUCCESS);

            }
            else
            {
                this.AddNotification("Der Import war fehlerhaft", NotificationType.ERROR);

            }
            return this.RedirectToAction("Index");

        }
    }
}
