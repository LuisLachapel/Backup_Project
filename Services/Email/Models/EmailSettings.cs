

namespace Services.Email.Models
{
    public class EmailSettings
    {
        public string email { get; set; } = string.Empty;
        public string host {  get; set; } = string.Empty;
        public int port { get; set; }
        public string baseUrlClient { get; set; } = string.Empty;
    }
}
