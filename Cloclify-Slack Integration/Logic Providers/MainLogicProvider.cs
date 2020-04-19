using Cloclify_Slack_Integration.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Logic_Providers
{
    public class MainLogicProvider : IMainLogicProvider
    {
        IStartupKeyManager startupKeyManager;
        IClockifyService clockifyService;

        InitApiKeyForm initApiKeyForm;

        public MainLogicProvider(IStartupKeyManager startupKeyManager, IClockifyService clockifyService, InitApiKeyForm initApiKeyForm)
        {
            this.startupKeyManager = startupKeyManager;
            this.clockifyService = clockifyService;

            this.initApiKeyForm = initApiKeyForm;
        }

        public bool TryInitializeApiKey()
        {
            bool isKeyInitialized = this.startupKeyManager.InitializeApiKey();

            if (!isKeyInitialized)
            {
                initApiKeyForm.ShowDialog();
            }

            return true;
        }

        public async Task<Tuple<string, List<Workspace>>> GetUserDataAsync()
        {
            this.clockifyService.InitApiKey(this.startupKeyManager.ApiKey);

            return await GetUserDataParallelAsync();
        }

        private async Task<Tuple<string, List<Workspace>>> GetUserDataParallelAsync()
        {
            Task<string> usernameTask = Task.Run(() => this.clockifyService.GetUserNameAsync());
            Task<List<Workspace>> workspacesTask = Task.Run(() => this.clockifyService.GetWorkspacesAsync());

            await Task.WhenAll(usernameTask, workspacesTask);

            string username = usernameTask.Result;
            List<Workspace> workspaces = workspacesTask.Result;

            return new Tuple<string, List<Workspace>>(username, workspaces);
        }

        public async Task<List<ClockifyTask>> GetRecordAsync(Workspace workspace, DateTime startDate)
        {
            List<TimeEntry> timeEntries = await this.clockifyService.GetDailyRecordAsync(workspace, startDate);
            Tuple<List<string>, List<string>> ids = this.FilterIds(timeEntries);

            List<string> projectIds = ids.Item1;
            List<string> taskIds = ids.Item2;

            List<ClockifyProject> allProjects = await this.clockifyService.GetProjectsAsync(workspace, projectIds);
            allProjects = await this.clockifyService.GetProjectsWithTasksAsync(workspace, allProjects);

            List<ClockifyTask> activeTasksForCurrentRecord = this.GetActiveTasksForCurrentRecord(taskIds, allProjects);

            List<ClockifyTask> calculatedTasks = CalculateTotalTaskTimes(timeEntries, activeTasksForCurrentRecord);
            return calculatedTasks;
        }

        private Tuple<List<string>, List<string>> FilterIds(List<TimeEntry> entries)
        {
            List<string> projectIds = new List<string>();
            List<string> taskIds = new List<string>();

            foreach (TimeEntry e in entries)
            {
                if (!projectIds.Contains(e.ProjectId))
                    projectIds.Add(e.ProjectId);

                if (!taskIds.Contains(e.TaskId))
                    taskIds.Add(e.TaskId);
            }

            return new Tuple<List<string>, List<string>>(projectIds, taskIds);
        }

        private List<ClockifyTask> GetActiveTasksForCurrentRecord(List<string> taskIds, List<ClockifyProject> projects)
        {
            List<ClockifyTask> activeTasksInCurrentRecord = new List<ClockifyTask>();

            foreach (ClockifyProject p in projects)
            {
                foreach (ClockifyTask t in p.Tasks)
                {
                    if (taskIds.Contains(t.Id))
                        activeTasksInCurrentRecord.Add(t);
                }
            }

            return activeTasksInCurrentRecord;
        }

        private List<ClockifyTask> CalculateTotalTaskTimes(List<TimeEntry> timeEntries, List<ClockifyTask> tasks)
        {
            foreach (ClockifyTask task in tasks)
            {
                foreach (TimeEntry entry in timeEntries)
                {
                    if (entry.TaskId == task.Id)
                    {
                        TimeSpan entryTime = entry.TimeInterval.End - entry.TimeInterval.Start;
                        task.TotalTime += entryTime;
                    }
                }
            }

            return tasks;
        }

        public void LogRecord(List<ClockifyTask> tasks, DateTime date, string path)
        {
            string fileName = date.ToString("dd-MM-yyyy");

            using (StreamWriter sw = new StreamWriter(path + "/" + fileName + ".txt"))
            {
                foreach (ClockifyTask task in tasks)
                {
                    sw.Write(task.Name + ":  " + task.TotalTime.ToString() + "\n");
                }
            }
        }
    }
}
