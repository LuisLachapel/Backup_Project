
namespace Services.Users.Models
{
    public class GetAllUserModel
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int positionId { get; set; }
        public string position {  get; set; } = string.Empty ;
    }
}
