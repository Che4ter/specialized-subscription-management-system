using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using essentialAdmin.Models;
using essentialAdmin.Data.Models;

namespace essentialAdmin.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(essentialAdminContext context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
