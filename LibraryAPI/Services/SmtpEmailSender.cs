using System.Net;
using System.Net.Mail;
using LibraryAPI.Core.Interfaces;

namespace LibraryAPI.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string to, string subject, string htmlMessage)
        {
            var host = _config["Smtp:Host"];
            var port = int.Parse(_config["Smtp:Port"]);
            var user = _config["Smtp:User"];
            var pass = _config["Smtp:Pass"];
            var from = _config["Smtp:From"];

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = true
            };

            var msg = new MailMessage(from, to, subject, htmlMessage)
            {
                IsBodyHtml = true
            };
            await client.SendMailAsync(msg);
        }
    }
}
