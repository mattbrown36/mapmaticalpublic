using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace mlShared.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(string _from, string email, string subject, string message)
        {
            var client = new RestClient();
            var url = await Util.GetSettingAsync("mailgun-url") + "/messages";
            client.BaseUrl = new Uri(url);
            client.Authenticator = new HttpBasicAuthenticator("api", await Util.GetSettingAsync("mailgun-key"));
            var request = new RestRequest();
            request.AddParameter("from", _from + "@mapmatical.com");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("text", message);
            request.Method = Method.POST;

            var timeout = new TimeSpan(0,0,5);

            IRestResponse response = null;
            bool finished = false;
            client.ExecuteAsync(request, innerResponse => {
                response = innerResponse;
                finished = true;
            });

            var started = DateTime.UtcNow;
            while (!finished)
            {
                if (started + timeout < DateTime.UtcNow)
                {
                    throw new TimeoutException("Did not receive a response from mail gun within 5 seconds.");
                }
                await Task.Delay(10);
            }

            if (response == null)
            {
                throw new Exception("Null response from mailgun.");
            }
            else if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Restsharp did not complete.");
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Mailgun returned status code: " + response.StatusCode.ToString("g"));
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
