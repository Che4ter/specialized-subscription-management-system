using esencialAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace esencialAdminTests.UnitTests.Controller

{
    public class HomeControllerTests
    {
        [Fact]
        public void Index()
        {
            var homeController = new esencialAdmin.Controllers.HomeController();

            IActionResult result = homeController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error()
        {
            var homeController = new esencialAdmin.Controllers.HomeController();
            homeController.ControllerContext = new ControllerContext();
            homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            IActionResult result = homeController.Error();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            ErrorViewModel model = Assert.IsType<ErrorViewModel>(viewResult.Model);

            Assert.IsType<ViewResult>(result);

        }
    }
}
