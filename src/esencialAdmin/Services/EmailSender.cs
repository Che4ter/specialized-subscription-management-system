using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace esencialAdmin.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string toEmail, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_config["FromEmail"], _config["DisplayName"])
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.Subject = "esencialAdmin - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(_config["Domain"], int.Parse(_config["Port"])))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_config["UsernameEmail"], _config["UsernamePassword"]);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);

                var es = ex.Message;
                //do something here
            }
        }
    }
}
