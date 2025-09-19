

namespace Services.Notes.Models
{
    public class UserNoteModel
    {
        public int id { get; set; }
        public string username { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public List<Note> notes { get; set; } = new List<Note>();

        public class Note
        {
            public string title { get; set; } = string.Empty;
            public string description { get; set; } = string.Empty;
            public DateTime date { get; set; }
        }
    }
}
