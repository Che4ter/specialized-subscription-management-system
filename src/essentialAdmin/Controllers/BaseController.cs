using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using essentialAdmin.Data.Models;

namespace essentialAdmin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly essentialAdminContext _context;

        public BaseController(essentialAdminContext context)
        {
            _context = context;
        }
    }
}