using EmailApp.Models;

namespace EmailApp.Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
