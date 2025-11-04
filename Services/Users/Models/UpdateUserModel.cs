

namespace Services.Users.Models
{
    public class UpdateUserModel
    { 
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int positionId { get; set; }
        //public List<int>? permissionIds { get; set; }
    }
}
