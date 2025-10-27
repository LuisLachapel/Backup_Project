

namespace Services.Email.Models
{
    public class EmailSettings
    {
        public string Email { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string SmtpLogin { get; set; } = string.Empty;
        public int Port { get; set; }
        public string ApiKey { get; set; } = string.Empty;
    }

}
