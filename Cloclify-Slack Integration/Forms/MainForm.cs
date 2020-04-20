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
using Cloclify_Slack_Integration.Models;
using Cloclify_Slack_Integration.Startup;
using System.Collections;

namespace Cloclify_Slack_Integration
{
    public partial class MainForm : Form
    {
        IMainLogicProvider logicProvider;
        IConfigManager configManager;

        string welcomeMessage = "Hello ";
        int defaultWorkspaceSelectionIndex = 0;

        bool isInitialLoad = true;

        public MainForm(IMainLogicProvider logicProvider, IConfigManager configManager)
        {
            InitializeComponent();

            this.logicProvider = logicProvider;
            this.configManager = configManager;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await this.InitFormDisplay();
        }

        private async Task InitFormDisplay()
        {
            this.statusLabel.Visible = false;

            bool isKeyInitialized = logicProvider.TryInitializeApiKey();

            if (isKeyInitialized)
            {
                UserData userData = await logicProvider.GetUserDataAsync();

                this.DisplayUserPersonalData(userData.UserName, userData.Workspaces);
                this.DisplayConfig(userData);
            }
        }

        public void DisplayUserPersonalData(string username, List<Workspace> workspaces)
        {
            this.usernameLabel.Text = this.welcomeMessage + username;

            this.workspacesDropdown.Items.AddRange(workspaces.ToArray());
        }

        public void DisplayConfig(UserData userData)
        {
            Config config = this.configManager.ReadConfig();

            if (config.Workspace != null)
                this.workspacesDropdown.SelectedIndex = this.workspacesDropdown.Items.IndexOf(userData.Workspaces.FirstOrDefault(w => w.Id == config.Workspace.Id));
            else
                this.workspacesDropdown.SelectedIndex = this.defaultWorkspaceSelectionIndex;

            this.selectedProjectsListBox.Items.AddRange(config.Projects.ToArray());
            this.savePathTextBox.Text = config.SaveFolderPath;
        }

        private async void getRecordsButton_Click(object sender, EventArgs e)
        {
            DateTime currentDate = this.datePicker.Value.Date;
            string path = this.savePathTextBox.Text;
            List<ClockifyProject> selectedProjects = this.selectedProjectsListBox.Items.Cast<ClockifyProject>().ToList();

            bool isPathInitialized = path != "";
            bool isDateLessThanNow = currentDate <= DateTime.Now.Date;
            bool isAnyProjectSelected = selectedProjects.Count > 0;

            if (isPathInitialized && isDateLessThanNow && isAnyProjectSelected)
            {
                Workspace selectedWorkspace = this.workspacesDropdown.SelectedItem as Workspace;

                try
                {
                    List<ClockifyTask> calculatedTasks = await this.logicProvider.GetRecordAsync(selectedWorkspace, currentDate, selectedProjects);
                    this.logicProvider.LogRecord(calculatedTasks, currentDate, path);

                    DisplayStatus(true);

                }
                catch (Exception ex)
                {
                    DisplayStatus(false);
                }
            }
            else
            {
                DisplayInputErrors(isPathInitialized, isDateLessThanNow, isAnyProjectSelected);
            }
        }

        private void DisplayStatus(bool isSuccesStatus)
        {
            if (isSuccesStatus)
            {
                this.statusLabel.Text = "Record succesfully logged!";
                this.statusLabel.ForeColor = Color.Green;
            }
            else
            {
                this.statusLabel.Text = "An error has occured";
                this.statusLabel.ForeColor = Color.Red;
            }

            this.statusLabel.Visible = true;
        }

        private void DisplayInputErrors(bool isPathInitialized, bool isDateLessThanNow, bool isAnyProjectSelected)
        {
            if (!isPathInitialized)
                errorProvider.SetError(this.selectFolderButton, "A folder must be selected");

            if (!isDateLessThanNow)
                errorProvider.SetError(this.datePicker, "Date cannot be after current one");

            if (!isAnyProjectSelected)
                errorProvider.SetError(this.selectedProjectsListBox, "At least one project must be selected");
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult res = this.folderDialog.ShowDialog();

            if(res == DialogResult.OK)
                savePathTextBox.Text = this.folderDialog.SelectedPath;
        }

        private void allProjectsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = allProjectsListBox.IndexFromPoint(e.Location);

            if(index != -1)
            {
                selectedProjectsListBox.Items.Add(allProjectsListBox.Items[index]);
            }
        }

        private void selectedProjectsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = selectedProjectsListBox.IndexFromPoint(e.Location);

            if (index != -1)
            {
                selectedProjectsListBox.Items.RemoveAt(index);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config config = new Config
            {
                Workspace = this.workspacesDropdown.SelectedItem as Workspace,
                SaveFolderPath = this.savePathTextBox.Text,
                Projects = this.selectedProjectsListBox.Items.Cast<ClockifyProject>().ToList()
            };

            this.configManager.WriteConfig(config);
        }

        private async void workspacesDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Workspace workspace = this.workspacesDropdown.SelectedItem as Workspace;

            List<ClockifyProject> allProjects = await this.logicProvider.GetProjectsForWorkspace(workspace);
       
            this.Invoke(new MethodInvoker(delegate ()
            {
                this.allProjectsListBox.Items.Clear();
                this.allProjectsListBox.Items.AddRange(allProjects.ToArray());
                if(!isInitialLoad)
                    this.selectedProjectsListBox.Items.Clear();

                this.isInitialLoad = false;
            }));
        }
    }
}
