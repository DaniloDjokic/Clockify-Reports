using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration
{
    public class ClockifyService
    {
        WebClient webClient;

        string user;

        public ClockifyService(WebClient webClient)
        {
            this.webClient = webClient;
        }

        public async Task<bool> ConfigureApiKey(string apiKey)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add("X-Api-Key", apiKey);

            try
            {
                string res = await this.webClient.SendRequest("GET", "user", headers);
            }
            catch(HttpRequestException e)
            {
                return false;
            }

            this.webClient.AddDefaultHeader("X-Api-Key", apiKey);
            return true;
        }

        public async Task<string> GetUserName()
        {
            try
            {
                string stringresult = await this.webClient.SendRequest("GET", "user");
                dynamic userData = JsonConvert.DeserializeObject(stringresult);
                string username = userData.name;
                return username;
            }
            catch (HttpRequestException e)
            {
                return "";
            }
        }
    }
}
