using essentialAdmin.Models.CustomerViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace essentialAdmin.Services
{
    public interface ICustomerService
    {
        int createNewCustomer(CustomerInputModel newCustomer);

        CustomerInputModel loadCustomerInputModel(int id);

        bool updateCustomer(CustomerInputModel customerToUpdate);

        bool deleteCustomer(int id);

        JsonResult loadCustomerDataTable(HttpRequest Request);
    }
}