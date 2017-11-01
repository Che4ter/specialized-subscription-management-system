using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using esencialAdmin.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace esencialAdmin.Controllers
{
    [Authorize(Policy = "RequireEmployeeRole")]
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}