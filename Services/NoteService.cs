using Entity;
using Persistence;

namespace Services
{
    public class NoteService : INoteService
    {
        private readonly INotePersistence _function;

        public NoteService(INotePersistence function)
        {
            _function = function;
        }

        public int Delete(int id)
        {
           int response = _function.DeleteNote(id);
            if (response <= 0) {
                throw new ArgumentException("No se ha eliminado el registro");
            }
            return response;
        }

        public List<Note> GetAllNotes()
        {
           return _function.GetAllNotes();
        }

        public Note GetById(int id)
        {
            return _function.GetById(id);
        }

        public DateTime? GetMaxDate()
        {
            return _function.GetMaxDate();
        }

        public List<Note> GetNotesByDate(DateTime? startDate, DateTime? endDate)
        {
            return _function.GetNotesByDate(startDate, endDate);
        }

        public void InsertNote(Note note)
        {
            note.date = DateTime.Now;

            _function.InsertNote(note);
        }

        public void updateNote(int id, Note note)
        {
           var noteExist =  _function.GetById(id);
            if(noteExist != null)
            {
                if (String.IsNullOrEmpty(note.title))
                {
                    throw new ArgumentException("El título es obligatorio");
                }
                note.id = id;

                
                _function.UpdateNote(note);
            }

        }
    }
}
