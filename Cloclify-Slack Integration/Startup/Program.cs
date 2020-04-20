using Cloclify_Slack_Integration.Interfaces;
using Cloclify_Slack_Integration.Logic_Providers;
using Cloclify_Slack_Integration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloclify_Slack_Integration
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HttpClient httpClient = new HttpClient();

            IStartupKeyManager startupKeyManager = new StartupKeyManager();
            IWebClient webClient = new WebClient(httpClient, "https://api.clockify.me/api/v1/");
            IClockifyService clockifyService = new ClockifyService(webClient);


            IInitLogicProvider initLogicProvider = new InitLogicProvider(startupKeyManager, clockifyService);
            InitApiKeyForm initApiKeyForm = new InitApiKeyForm(initLogicProvider);

            IMainLogicProvider mainLogicProvider = new MainLogicProvider(startupKeyManager, clockifyService, initApiKeyForm);
            IConfigManager configManager = new ConfigManager();

            Application.Run(new MainForm(mainLogicProvider, configManager));
        }
    }
}
