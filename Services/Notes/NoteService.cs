using Entity;
using Services.Notes.Models;
using Services.Users;
using Persistence.Notes;

namespace Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly INotePersistence _function;
        private readonly IUserService _userService;

        public NoteService(INotePersistence function, IUserService userService)
        {
            _function = function;
            _userService = userService;
        }

        public int Delete(int id)
        {
           int response = _function.DeleteNote(id);
            if (response <= 0) {
                throw new ArgumentException("No se ha eliminado el registro");
            }
            return response;
        }

        public List<GetNoteModel> GetAllNotes()
        {
           var notes = _function.GetAllNotes();

            return notes.Select(note => new GetNoteModel
            {
                id = note.id,
                title = note.title,
                description = note.description,
                date = note.date,
                userId = note.userId,
                username = note?.user.name ?? string.Empty

            }).ToList();
        }

        public GetNoteModel GetById(int id)
        {
            var note = _function.GetById(id);
            if (note == null)
            {
                return null;
            }

            return new GetNoteModel
            {
                id = note.id,
                title = note.title,
                description = note.description,
                date = note.date,
                userId = note.userId,
                username = note?.user.name ?? string.Empty
            };
        }

        public DateTime? GetMaxDate()
        {
            return _function.GetMaxDate();
        }

        public List<GetNoteModel> GetNotesByDate(DateTime? startDate, DateTime? endDate)
        {
            var notes = _function.GetNotesByDate(startDate, endDate);
            return notes.Select(note => new GetNoteModel
            {
                id = note.id,
                title = note.title,
                description = note.description,
                date = note.date,

            }).ToList();

        }

        public void InsertNote(CreateNoteModel model)
        {
            var userExist = _userService.GetUserById(model.userId);
            if(userExist == null)
            {
                throw new ArgumentException("No existe un usuario con este id");
            }
            var note = new Note
            {
               title = model.title,
               description = model.description,
               userId = model.userId,
               date = DateTime.Now
            };
            

            _function.InsertNote(note);
        }

        public void updateNote(int id, UpdateNoteModel model)
        {

                var userExist = _userService.GetUserById(model.userId);
                if (userExist == null)
                {
                    throw new ArgumentException("No existe un usuario con este id");
                }
                var note = new Note
                {
                    id = id,
                    title = model.title,
                    description = model.description,
                    userId = model.userId,


                };

                _function.UpdateNote(note);
            

        }
    }
}
