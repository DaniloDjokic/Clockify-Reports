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
        User user;

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

        public void InitApiKey(string apiKey)
        {
            this.webClient.AddDefaultHeader("X-Api-Key", apiKey);
        }

        public async Task<string> GetUserName()
        {
            try
            {
                string stringresult = await this.webClient.SendRequest("GET", "user");
                user = JsonConvert.DeserializeObject<User>(stringresult);
                string username = user.Name;
                return username;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }

        public async Task<List<Workspace>> GetWorkspaces()
        {
            try
            {
                string stringResult = await this.webClient.SendRequest("GET", "workspaces");
                List<Workspace> workspaces = JsonConvert.DeserializeObject<List<Workspace>>(stringResult);
                return workspaces;
            }
            catch(HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<List<TimeEntry>> GetRecord(Workspace workspace, DateTime startDate)
        {
            try
            {
                string formatedDate = startDate.ToString("yyyy-MM-dd");
                formatedDate += "T00:00:00Z";
                string endpoint = $"workspaces/{workspace.Id}/user/{this.user.Id}/time-entries";

                Dictionary<string, string> queryParams = new Dictionary<string, string>(); ;
                queryParams["start"] = formatedDate;

                string stringResult = await this.webClient.SendRequest("GET", endpoint, null, queryParams);
                List<TimeEntry> timeEntries = JsonConvert.DeserializeObject<List<TimeEntry>>(stringResult);

                return timeEntries;
            }
            catch(HttpRequestException e)
            {
                return null;
            }
        } 
    }
}
