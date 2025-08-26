using Services.Notes.Models;


namespace Services.Notes
{
    public interface INoteService
    {
        List<GetNoteModel> GetAllNotes();
        GetNoteModel GetById(int id);

        void InsertNote(CreateNoteModel note);
        void updateNote(int  id, UpdateNoteModel note);
        int Delete(int id);
        List<GetNoteModel> GetNotesByDate(DateTime? startDate, DateTime? endDate);
        DateTime? GetMaxDate();

    }
}
