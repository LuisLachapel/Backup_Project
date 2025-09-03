
using Services.Users.Models;
using System.Data;
namespace Services.Files
{
    public interface IFileService
    {
        byte[] UserReport(DateTime? startDate, DateTime? endDate);
        byte[] ExcelTable(DateTime? startDate, DateTime? endDate);
    }
}
