using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Models;
using esencialAdmin.Services;

namespace esencialAdmin.Controllers
{
    public class HomeController : BaseController
    {
        private IStatisticService _sService;

        public HomeController(IStatisticService sService)
        {
            _sService = sService;
        }

        public IActionResult Index()
        {
            return View(_sService.getOverViewModel());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}