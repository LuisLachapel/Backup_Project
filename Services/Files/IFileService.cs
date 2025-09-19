
namespace Services.Files
{
    public interface IFileService
    {
        byte[] UserReport(DateTime? startDate, DateTime? endDate);
        byte[] ExcelUserReport(DateTime? startDate, DateTime? endDate);
        byte[] NoteReport(DateTime? startDate, DateTime? endDate);
        byte[] ExcelNoteReport(DateTime? startDate, DateTime? endDate);
    }
}
