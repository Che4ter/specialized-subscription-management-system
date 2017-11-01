using esencialAdmin.Models.CustomerViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace esencialAdmin.Services
{
    public interface ICustomerService
    {
        int createNewCustomer(CustomerInputModel newCustomer);

        CustomerInputModel loadCustomerInputModel(int id);

        bool updateCustomer(CustomerInputModel customerToUpdate);

        bool deleteCustomer(int id);

        JsonResult loadCustomerDataTable(HttpRequest Request);

        bool isEmailUnique(string email);

        bool isEmailUnique(string email, int id);
    }
}