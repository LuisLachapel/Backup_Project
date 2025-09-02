
namespace Persistence.Users
{
    public class UserNotesSummary
    {
        public string name {  get; set; } = string.Empty;
        public string position {  get; set; } = string.Empty;
        public int records { get; set; }
        public string status { get; set; } = string.Empty;
        public DateTime creationDate { get; set; }
    }
}
