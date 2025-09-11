using Services.Email.Models;

namespace Services.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailService(EmailMessage message);
    }
}
