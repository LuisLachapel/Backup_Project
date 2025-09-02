
using Services.Users.Models;
namespace Services.PDF
{
    public interface IPdfService
    {
        byte[] UserReport(DateTime? startDate, DateTime? endDate);
    }
}
