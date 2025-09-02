

namespace Services.Email.Models
{
    public class SendGridEmailRequestModel
    {
        public ContentEmail From { get; set; }
        public List<Personalization> Personalizations { get; set; }
        public List<ContentBody> Content { get; set; }

    }
    public class Personalization
    {
        public string subject { get; set; } = string.Empty;
        public List<ContentEmail> To { get; set; }
    }

    public class ContentEmail
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class ContentBody
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
