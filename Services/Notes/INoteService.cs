
using Entity;

namespace Services.Notes
{
    public interface INoteService
    {
        List<Note> GetAllNotes();
        Note GetById(int id);

        void InsertNote(Note note);
        void updateNote(int  id, Note note);
        int Delete(int id);
        List<Note> GetNotesByDate(DateTime? startDate, DateTime? endDate);
        DateTime? GetMaxDate();

    }
}
