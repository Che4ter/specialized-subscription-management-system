using esencialAdmin.Controllers;
using esencialAdmin.Data.Models;
using esencialAdmin.Extensions;
using esencialAdmin.Models;
using esencialAdmin.Models.CustomerViewModels;
using esencialAdmin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace esencialAdminTests.UnitTests.Controller

{

    public class CustomerControllerTests
    {
        [Fact]
        public void Index()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerService>();

            var customerController = new CustomerController(mockRepo.Object);

            IActionResult result = customerController.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetCreate()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerService>();

            var customerController = new CustomerController(mockRepo.Object);

            IActionResult result = customerController.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PostCreateEmptyModel()
        {
            var mockRepo = new Mock<ICustomerService>();
            var customerModel = new CustomerInputModel();
            var customerController = new CustomerController(mockRepo.Object);
            customerController.ControllerContext = new ControllerContext();
            customerController.ControllerContext.HttpContext = new DefaultHttpContext();
            IActionResult result = customerController.Create(customerModel);
            
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            //Assert.True(viewResult.TempData.ContainsKey(NotificationType.ERROR));
        }

        [Fact]
        public void PostCreate()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

                UserResolverService userResolverService = new UserResolverService(httpContextAccessor);

                var options = new DbContextOptionsBuilder<esencialAdminContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new esencialAdminContext(options, userResolverService))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new esencialAdminContext(options, userResolverService))
                {
                    var mockRepo = new Mock<ICustomerService>();
                    mockRepo.Setup(repo => repo.isEmailUnique("test@tasdfsfdest.com")).Returns(true);

                    var customerModel = new CustomerInputModel()
                    {
                        Title = "Herr",
                        FirstName = "TestVorname",
                        LastName = "TestLastName",
                        Street = "TestStrasse,",
                        Zip = "1234",
                        City = "TestOrt",
                        Company = "TestFirma",
                        Email = "test@tasdfsfdest.com",
                        Phone = "0791234567",
                        GeneralRemarks = "TestBemerkungen",
                        PurchasesRemarks = "TestEinkäufe"
                    };
                    var customerController = new CustomerController(mockRepo.Object);
                    IActionResult result = customerController.Create(customerModel);
                    ViewResult viewResult = Assert.IsType<ViewResult>(result);
                }

            }
            finally
            {
                connection.Close();
            }
     
        }
    }
}
