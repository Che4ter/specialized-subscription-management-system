using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
