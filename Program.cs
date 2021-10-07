using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendEmail
{
    class program
    {
        static void Main(string [] args)
        {
            Execute().Wait();
        }

        static async Task Execute(){
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY"); // Store the key first
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Tester");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("manthiladevflow@gmail.com", "Manthila");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}