using Services.Email.Models;
using System.Text;
using Newtonsoft.Json;
using DotNetEnv;

namespace Services.Email
{
    public class SendGridEmailService : ISendGridEmailService
    {
        public async Task<bool> Execute(SendGridEmailRequestModel model)
        {
            string apiUrl = "https://api.sendgrid.com/v3/mail/send";
            Env.Load(@"C:\Users\llachapell\source\repos\Backup_Project\.env");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            client.DefaultRequestHeaders.Add("Accept", $"application/json");

            string emailContent = JsonConvert.SerializeObject(model);
            var response = await client.PostAsync(apiUrl, new StringContent(emailContent, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
