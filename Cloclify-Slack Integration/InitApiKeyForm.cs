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
    public partial class InitApiKeyForm : Form
    {
        MainForm mainForm;
        IStartupKeyManager startupKeyManager;
        ClockifyService clockifyService;
        bool isApiKeyConfigured;

        public InitApiKeyForm(MainForm mainForm, IStartupKeyManager startupKeyManager, ClockifyService clockifyService)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.startupKeyManager = startupKeyManager;
            this.clockifyService = clockifyService;
            isApiKeyConfigured = false;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            string apiKey = keyInputTextBox.Text;
            isApiKeyConfigured = await clockifyService.ConfigureApiKey(apiKey);

            bool writeRes = startupKeyManager.WriteApiKey(apiKey);

            if(writeRes)
                await this.LoginUser();
        }

        private async Task LoginUser()
        {
            string username = await this.clockifyService.GetUserName();

            mainForm.DisplayUser(username);
            this.Close();
        }

        private void InitApiKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isApiKeyConfigured)
                Application.Exit();
        }
    }
}
