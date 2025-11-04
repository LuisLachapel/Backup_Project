using System.Collections.ObjectModel;

namespace Entity
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string password {  get; set; } = string.Empty;
        public int positionId { get; set; }
        public Position? position { get; set; }

        public DateTime creationDate { get; set; }
        public Collection<Note>? notes { get; set; }



    }
}
