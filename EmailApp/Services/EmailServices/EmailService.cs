using EmailApp.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace EmailApp.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDTO request)
        {
            Random r = new Random();
            int verificationCode = r.Next(100000, 1000000);
                
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email")["Username"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = $"Registro en IOON [{verificationCode}]";
            email.Body = new TextPart(TextFormat.Html) { Text = $" Your verification code is: { verificationCode }" };

            
            var smtp = new SmtpClient();
            using (smtp)
            {
                smtp.CheckCertificateRevocation = false;
                smtp.Connect(_config.GetSection("Email")["Host"], 587, SecureSocketOptions.Auto);
                smtp.Authenticate(_config.GetSection("Email")["Username"], _config.GetSection("Email")["Password"]);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
