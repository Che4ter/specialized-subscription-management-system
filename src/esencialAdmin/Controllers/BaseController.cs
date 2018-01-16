using Microsoft.AspNetCore.Mvc;
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