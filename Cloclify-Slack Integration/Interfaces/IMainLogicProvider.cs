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
        Task<Tuple<string, List<Workspace>>> GetUserDataAsync();
        Task<List<ClockifyTask>> GetRecordAsync(Workspace workspace, DateTime startDate);
        void LogRecord(List<ClockifyTask> tasks, DateTime date, string path);
    }
}
