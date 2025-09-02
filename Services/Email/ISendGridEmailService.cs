using Services.Email.Models;
namespace Services.Email
{
    public interface ISendGridEmailService
    {
        Task<bool> Execute(SendGridEmailRequestModel model);
    }
}
