
using Services.Email.Models;
using FluentEmail.Core;

namespace Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        

        public EmailService(IFluentEmail email)
        {
            _fluentEmail = email;
           
        }
        public async Task<bool> SendEmailService(EmailMessage message)
        {

            var email =  _fluentEmail.To(message.to).Subject(message.subject).Body(message.body, isHtml: true);
            if (message.attachments != null && message.attachments.Any())
            {
                foreach (var file in message.attachments)
                {
                    using var tempStream = new MemoryStream();
                    await file.CopyToAsync(tempStream);

                    var bytes = tempStream.ToArray();
                    var attachmentStream = new MemoryStream(bytes);

                    email.Attach(new FluentEmail.Core.Models.Attachment
                    {
                        Data = attachmentStream,
                        Filename = file.FileName,
                        ContentType = file.ContentType
                    });
                }
            }


            var response = await email.SendAsync();
             return response.Successful;

        }
    }
}
