using Microsoft.AspNetCore.Http;

namespace esencialAdmin.Services
{
    public interface IImportService
    {
        int importCustomer(IFormFile formFile);
    }
}