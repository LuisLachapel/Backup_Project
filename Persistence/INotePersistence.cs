using Entity;

namespace Persistence
{
    public interface INotePersistence
    {
        List<Note> GetAllNotes(); 
        Note GetById(int id);
        void InsertNote(Note note);

        void UpdateNote(Note note);
        int DeleteNote(int id);

        List<Note> GetNotesByDate(DateTime? startDate, DateTime? endDate);

        DateTime? GetMaxDate();

    }
}
