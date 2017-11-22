using Microsoft.AspNetCore.Mvc;

namespace esencialAdmin.Services
{
    public interface IImageService
    {
        FileStreamResult loadSubscriptionImage(string URL);

        bool deleteSubscriptionPhoto(int fileID);
    }
}