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
            initApiKeyForm = new InitApiKeyForm(this, startupKeyManager, clockifyService);

            InitApiKey();
        }

        private void InitApiKey()
        {
            bool isKeyInitialized = startupKeyManager.InitializeApiKey();
            if (!isKeyInitialized)
            {
                initApiKeyForm.ShowDialog();
            }
        }

        public void DisplayUser(string username)
        {
            this.usernameLabel.Text = this.welcomeMessage + username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
