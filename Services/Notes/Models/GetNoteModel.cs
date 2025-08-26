

namespace Services.Notes.Models
{
    public class GetNoteModel
    {
        public int id { get; set; }

        
        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;
        public DateTime date { get; set; }
        public int userId { get; set; }
        public string username { get; set; } = string.Empty;
    }
}
