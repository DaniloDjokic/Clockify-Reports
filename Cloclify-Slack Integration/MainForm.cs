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
            bool isDateLessThanNow = this.datePicker.Value.Date <= DateTime.Now.Date;

            if (isPathInitialized && isDateLessThanNow)
            {
                Workspace workspace = this.workspacesDropdown.SelectedItem as Workspace;

                List<TimeEntry> timeEntries = await this.clockifyService.GetRecord(workspace);
            }
            else
            {
                DisplayInputErrors(isPathInitialized, isDateLessThanNow);
            }
        }

        private void DisplayInputErrors(bool isPathInitialized, bool isDateLessThanNow)
        {
            if (!isPathInitialized)
                errorProvider.SetError(this.savePathTextBox, "A folder must be selected ");

            if (!isDateLessThanNow)
                errorProvider.SetError(this.selectFolderButton, "Date cannot be after current one ");
        }
    }
}
