using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Interfaces
{
    public interface IClockifyService
    {
        Task<bool> ConfigureApiKeyAsync(string apiKey);
        void InitApiKey(string apiKey);
        Task<string> GetUserNameAsync();
        Task<List<Workspace>> GetWorkspacesAsync();
        Task<List<TimeEntry>> GetDailyRecordAsync(Workspace workspace, DateTime startDate);
        Task<List<ClockifyProject>> GetProjectsAsync(Workspace workspace, List<string> projectIds);
        Task<List<ClockifyProject>> GetProjectsWithTasksAsync(Workspace workspace, List<ClockifyProject> projects);
    }
}
