using Cloclify_Slack_Integration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Logic_Providers
{
    public class InitLogicProvider : IInitLogicProvider
    {
        IStartupKeyManager startupKeyManager;
        IClockifyService clockifyService;

        private bool isApiKeyConfigured;
        public bool IsApiKeyConfigured { get { return this.isApiKeyConfigured; } }

        public InitLogicProvider(IStartupKeyManager startupKeyManager, IClockifyService clockifyService)
        {
            this.startupKeyManager = startupKeyManager;
            this.clockifyService = clockifyService;
            
            this.isApiKeyConfigured = false;
        }

        public async Task<bool> TryConfigureApiKey(string apiKey)
        {
            isApiKeyConfigured = await clockifyService.ConfigureApiKeyAsync(apiKey);

            if (isApiKeyConfigured)
            {
                return startupKeyManager.WriteApiKey(apiKey);
            }

            return false;
        }

        private async Task<Tuple<string, List<Workspace>>> GetUserInfo()
        {
            Task<string> usernameTask = Task.Run(() => this.clockifyService.GetUserNameAsync());
            Task<List<Workspace>> workspacesTask = Task.Run(() => this.clockifyService.GetWorkspacesAsync());

            await Task.WhenAll(usernameTask, workspacesTask);

            string username = usernameTask.Result;
            List<Workspace> workspaces = workspacesTask.Result;

            return new Tuple<string, List<Workspace>>(username, workspaces);
        }
    }
}
