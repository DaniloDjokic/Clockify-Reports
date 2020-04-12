using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloclify_Slack_Integration
{
    public partial class MainForm : Form
    {
        IStartupKeyManager startupKeyManager;
        InitApiKeyForm initApiKeyForm;
        ClockifyService clockifyService;

        string welcomeMessage = "Hello ";

        public MainForm(IStartupKeyManager startupKeyManager, ClockifyService clockifyService)
        {
            InitializeComponent();

            this.startupKeyManager = startupKeyManager;
            this.clockifyService = clockifyService;

            initApiKeyForm = new InitApiKeyForm(this, startupKeyManager, clockifyService);

            Task.Run(() => InitFormDisplay()).Wait();
        }

        private async Task InitFormDisplay()
        {
            bool isKeyInitialized = startupKeyManager.InitializeApiKey();
            if (!isKeyInitialized)
            {
                initApiKeyForm.ShowDialog();
            }
            else
            {
                await DisplayConfiguredKey();
            }
        }

        private async Task DisplayConfiguredKey()
        {
            this.clockifyService.InitApiKey(this.startupKeyManager.ApiKey);

            await GetUserDataParallelAsync();
        }

        private async Task GetUserDataParallelAsync()
        {
            Task<string> usernameTask = Task.Run(() => this.clockifyService.GetUserName());
            Task<List<Workspace>> workspacesTask = Task.Run(() => this.clockifyService.GetWorkspaces());

            await Task.WhenAll(usernameTask, workspacesTask);

            string username = usernameTask.Result;
            List<Workspace> workspaces = workspacesTask.Result;

            DisplayUser(username);
            DisplayWorkspaces(workspaces);
        }

        public void DisplayUser(string username)
        {
            this.usernameLabel.Text = this.welcomeMessage + username;
        }

        public void DisplayWorkspaces(List<Workspace> workspaces)
        {
            this.workspacesDropdown.DataSource = workspaces;
            this.workspacesDropdown.SelectedIndex = 0;
        }

        private async void getRecordsButton_Click(object sender, EventArgs e)
        {
            bool isPathInitialized = true; // this.savePathTextBox.Text != "";
            bool isDateLessThanNow = this.datePicker.Value.Date < DateTime.Now.Date;

            if (isPathInitialized && isDateLessThanNow)
            {
                Workspace workspace = this.workspacesDropdown.SelectedItem as Workspace;
                DateTime startDate = this.datePicker.Value.Date;
                
                List<TimeEntry> timeEntries = await this.clockifyService.GetRecord(workspace, startDate);
                CalculateTotalTime(timeEntries);
            }
            else
            {
                DisplayInputErrors(isPathInitialized, isDateLessThanNow);
            }
        }

        private void CalculateTotalTime(List<TimeEntry> timeEntries)
        {
            TimeSpan totalTime = new TimeSpan(0, 0, 0);

            foreach(TimeEntry entry in timeEntries)
            {
                TimeSpan time = ConvertDurationToDateTime(entry.TimeInterval.Duration);
                totalTime += time;
            }

            string x = "dsa";
        }

        private TimeSpan ConvertDurationToDateTime(string duration)
        {
            string durationSubstring = duration.Substring(2);

            bool containsHour = false;
            bool containsMinute = false;

            int hours = 0, minutes = 0, seconds = 0;

            if (durationSubstring.Contains("H"))
            {
                containsHour = true;
                hours = Int32.Parse(durationSubstring.Substring(0, durationSubstring.IndexOf('H')));
            }
            if (durationSubstring.Contains("M"))
            {
                containsMinute = true;

                if (containsHour)
                {
                    minutes = GetSubstringDigits(durationSubstring, 'H', 'M');
                }
                else
                    minutes = Int32.Parse(durationSubstring.Substring(0, durationSubstring.IndexOf('M')));
            }
            if (durationSubstring.Contains("S"))
            {
                if (containsMinute)
                {
                    seconds = GetSubstringDigits(durationSubstring, 'M', 'S');
                }
                else if (containsHour)
                {
                    seconds = GetSubstringDigits(durationSubstring, 'H', 'S');
                }
                else
                    seconds = Int32.Parse(durationSubstring.Substring(0, durationSubstring.IndexOf('S')));
            }


            string[] timeValues = durationSubstring.Split('H', 'M', 'S');

            TimeSpan timeSpan = new TimeSpan(hours, minutes, seconds);

            return timeSpan;
        }

        private int GetSubstringDigits(string inputString, char leftBoundry, char rightBoundry)
        {
            int digits;

            bool isSingleDigit = inputString.IndexOf(rightBoundry) - inputString.IndexOf(leftBoundry) == 1;

            if (isSingleDigit)
                digits = Int32.Parse(inputString.Substring(inputString.IndexOf(leftBoundry) + 1, 1));
            else
                digits = Int32.Parse(inputString.Substring(inputString.IndexOf(leftBoundry) + 1, 2));

            return digits;
        }

        private void DisplayInputErrors(bool isPathInitialized, bool isDateLessThanNow)
        {
            if (!isPathInitialized)
                errorProvider.SetError(this.selectFolderButton, "A folder must be selected ");

            if (!isDateLessThanNow)
                errorProvider.SetError(this.datePicker, "Date cannot be after current one ");
        }
    }
}
