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

            IStartupKeyManager startupKeyManager = new StartupKeyManager();

            HttpClient httpClient = new HttpClient();

            WebClient webClient = new WebClient(httpClient, "https://api.clockify.me/api/v1/");
            ClockifyService clockifyService = new ClockifyService(webClient);

            Application.Run(new MainForm(startupKeyManager, clockifyService));
        }
    }
}
