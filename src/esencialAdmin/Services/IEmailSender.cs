using System.Threading.Tasks;

namespace esencialAdmin.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
