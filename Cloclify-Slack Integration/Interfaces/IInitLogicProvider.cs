using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Interfaces
{
    public interface IInitLogicProvider
    {
        bool IsApiKeyConfigured { get; }
        Task<bool> TryConfigureApiKey(string apiKey);
    }
}
