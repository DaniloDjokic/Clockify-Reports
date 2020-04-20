using Cloclify_Slack_Integration.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration
{
    public class ClockifyService : IClockifyService
    {
        IWebClient webClient;
        User activeUser;

        public ClockifyService(IWebClient webClient)
        {
            this.webClient = webClient;
        }

        public async Task<bool> ConfigureApiKeyAsync(string apiKey)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add("X-Api-Key", apiKey);

            try
            {
                dynamic res = await this.webClient.SendRequest<dynamic>("GET", "user", headers);
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

        public async Task<string> GetUserNameAsync()
        {
            try
            {
                activeUser = await this.webClient.SendRequest<User>("GET", "user");
                string username = activeUser.Name;
                return username;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }

        public async Task<List<Workspace>> GetWorkspacesAsync()
        {
            try
            {
                List<Workspace> workspaces = await this.webClient.SendRequest<List<Workspace>>("GET", "workspaces");
                return workspaces;
            }
            catch(HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<List<TimeEntry>> GetDailyRecordAsync(Workspace workspace, DateTime startDate)
        {
            try
            {
                string endpoint = $"workspaces/{workspace.Id}/user/{this.activeUser.Id}/time-entries";

                string timeFormat = "T00:00:00Z";

                string formatedStartDate = startDate.ToString("yyyy-MM-dd");
                string formatedEndDate = startDate.AddDays(1).ToString("yyyy-MM-dd");

                formatedStartDate += timeFormat;
                formatedEndDate += timeFormat;

                Dictionary<string, string> queryParams = new Dictionary<string, string>(); ;
                queryParams["start"] = formatedStartDate;
                queryParams["end"] = formatedEndDate;

                List<TimeEntry> timeEntries = await this.webClient.SendRequest<List<TimeEntry>>("GET", endpoint, null, queryParams);

                return timeEntries;
            }
            catch(HttpRequestException e)
            {
                return null;
            }
        } 

        public async Task<List<ClockifyProject>> GetProjectsAsync(Workspace workspace, List<string> projectIds = null)
        {
            string endpoint = $"workspaces/{workspace.Id}/projects";
            List<ClockifyProject> projects = await this.webClient.SendRequest<List<ClockifyProject>>("GET", endpoint);

            if(projectIds != null)
            {
                List<ClockifyProject> activeProjectsForCurrentRecord = new List<ClockifyProject>();
                foreach (ClockifyProject p in projects)
                {
                    if (projectIds.Contains(p.Id))
                        activeProjectsForCurrentRecord.Add(p);
                }
            }

            return projects;
        }

        public async Task<List<ClockifyProject>> GetProjectsWithTasksAsync(Workspace workspace, List<ClockifyProject> projects)
        {
            int maxConcurrencyCount = 10;
            List<Task> tasks = new List<Task>();

            string endpoint = $"workspaces/{workspace.Id}/projects/";

            using (SemaphoreSlim sem = new SemaphoreSlim(maxConcurrencyCount))
            {
                foreach (ClockifyProject project in projects)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await sem.WaitAsync();

                            List<ClockifyTask> projectTasks = await this.webClient.SendRequest<List<ClockifyTask>>("GET", endpoint + project.Id + "/tasks");
                            foreach (ClockifyTask task in projectTasks)
                                task.ProjectName = project.Name;
                            
                            project.Tasks = projectTasks;
                        }
                        finally
                        {
                            sem.Release();
                        }
                    }));
                }

                await Task.WhenAll(tasks);
                return projects;
            }
        }
    }
}
