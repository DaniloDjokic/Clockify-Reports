using Cloclify_Slack_Integration.Interfaces;
using Cloclify_Slack_Integration.Models;
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

        public async Task<UserData> GetUserDataAsync()
        {
            this.clockifyService.InitApiKey(this.startupKeyManager.ApiKey);

            return await GetUserDataParallelAsync();
        }

        private async Task<UserData> GetUserDataParallelAsync()
        {
            Task<string> usernameTask = Task.Run(() => this.clockifyService.GetUserNameAsync());
            Task<List<Workspace>> workspacesTask = Task.Run(() => this.clockifyService.GetWorkspacesAsync());

            await Task.WhenAll(usernameTask, workspacesTask);

            string username = usernameTask.Result;
            List<Workspace> workspaces = workspacesTask.Result;

            return new UserData { UserName = username, Workspaces = workspaces };
        }

        public async Task<List<ClockifyProject>> GetProjectsForWorkspace(Workspace workspace)
        {
            return await this.clockifyService.GetProjectsAsync(workspace);
        }

        public async Task<List<ClockifyTask>> GetRecordAsync(Workspace workspace, DateTime startDate, List<ClockifyProject> selectedProjects)
        {
            List<TimeEntry> timeEntries = await this.clockifyService.GetDailyRecordAsync(workspace, startDate);
            List<string> taskIds = this.FilterIds(timeEntries);

            selectedProjects = await this.clockifyService.GetProjectsWithTasksAsync(workspace, selectedProjects);

            List<ClockifyTask> activeTasksForCurrentRecord = this.GetActiveTasksForCurrentRecord(taskIds, selectedProjects);

            List<ClockifyTask> calculatedTasks = CalculateTotalTaskTimes(timeEntries, activeTasksForCurrentRecord);
            return calculatedTasks;
        }

        private List<string> FilterIds(List<TimeEntry> entries)
        {
            List<string> taskIds = new List<string>();

            foreach (TimeEntry e in entries)
            {
                if (!taskIds.Contains(e.TaskId))
                    taskIds.Add(e.TaskId);
            }

            return taskIds;
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
                if(tasks.Count == 0)
                {
                    sw.WriteLine("No time entries for selected projects...");
                    return;
                }

                foreach (ClockifyTask task in tasks)
                {
                    sw.Write(task.Name + ":  " + task.TotalTime.ToString() + "\n");
                }
            }
        }
    }
}
