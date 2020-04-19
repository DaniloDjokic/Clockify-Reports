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
using System.IO;
using Cloclify_Slack_Integration.Interfaces;

namespace Cloclify_Slack_Integration
{
    public partial class MainForm : Form
    {
        IMainLogicProvider logicProvider;

        string welcomeMessage = "Hello ";

        public MainForm(IMainLogicProvider logicProvider)
        {
            InitializeComponent();

            this.logicProvider = logicProvider;
            
            Task.Run(() => InitFormDisplay()).Wait();
        }

        private async Task InitFormDisplay()
        {
            bool isKeyInitialized = logicProvider.TryInitializeApiKey();

            if (isKeyInitialized)
            {
                Tuple<string, List<Workspace>> userData = await logicProvider.GetUserDataAsync();

                this.DisplayUser(userData.Item1);
                this.DisplayWorkspaces(userData.Item2);
            }
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
            DateTime currentDate = this.datePicker.Value.Date;
            string path = this.savePathTextBox.Text;

            bool isPathInitialized = path != "";
            bool isDateLessThanNow = currentDate <= DateTime.Now.Date;

            if (isPathInitialized && isDateLessThanNow)
            {
                Workspace selectedWorkspace = this.workspacesDropdown.SelectedItem as Workspace;

                List<ClockifyTask> calculatedTasks = await this.logicProvider.GetRecordAsync(selectedWorkspace, currentDate);
                this.logicProvider.LogRecord(calculatedTasks, currentDate, path);
            }
            else
            {
                DisplayInputErrors(isPathInitialized, isDateLessThanNow);
            }
        }

        private void DisplayInputErrors(bool isPathInitialized, bool isDateLessThanNow)
        {
            if (!isPathInitialized)
                errorProvider.SetError(this.selectFolderButton, "A folder must be selected ");

            if (!isDateLessThanNow)
                errorProvider.SetError(this.datePicker, "Date cannot be after current one ");
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult res = this.folderDialog.ShowDialog();

            if(res == DialogResult.OK)
                savePathTextBox.Text = this.folderDialog.SelectedPath;
        }
    }
}
