namespace Persistence.Notes.models
{
    public class NotesByUsers
    {
        public int id {  get; set; }
        public string username { get; set; } =  string.Empty;
        public string position { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime date { get; set; }

    }
}
