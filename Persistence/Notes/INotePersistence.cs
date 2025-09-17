using Entity;
using Persistence.Notes.models;
namespace Persistence.Notes
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
        List<NotesByUsers> GetNotesByUsers(DateTime? startDate, DateTime? endDate);

    }
}
