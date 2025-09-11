

using Microsoft.AspNetCore.Http;

namespace Services.Email.Models
{
    public class EmailMessage
    {
        public string to { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public List<IFormFile>? attachments { get; set; }
    }
}
