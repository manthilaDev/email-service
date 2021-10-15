using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NorthwindService.Controllers
{
    [ApiController]
    [Route("api/forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Get(5);
        }

        [HttpGet("{days:int}")]
        public IEnumerable<WeatherForecast> Get(int days)
        {
        var rng = new Random();
        return Enumerable.Range(1, days)
        .Select(index => new WeatherForecast
        {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();
        }
    }
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> getsome()
        {
            return new[]{"hello World !"};
        }

        [HttpPost]
        public void sendAway()
        {
            Console.WriteLine("Sent away!");
            EmailSender().Wait();

        }

        static async Task EmailSender()
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(); // API KEY Required 
            var from = new EmailAddress("manthiladevflow@gmail.com", "Manthila Test Send");
            var subject = "Sending with Twilio SendGrid is Fun";
            var to = new EmailAddress("manthiladevflow@gmail.com", "Manthila Test Recieve");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }   


    }
}
