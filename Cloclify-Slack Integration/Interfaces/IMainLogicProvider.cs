using Cloclify_Slack_Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Interfaces
{
    public interface IMainLogicProvider
    {
        bool TryInitializeApiKey();
        Task<UserData> GetUserDataAsync();
        Task<List<ClockifyProject>> GetProjectsForWorkspace(Workspace workspace);
        Task<List<ClockifyTask>> GetRecordAsync(Workspace workspace, DateTime startDate, List<ClockifyProject> selectedProjects);
        void LogRecord(List<ClockifyTask> tasks, DateTime date, string path);
    }
}
