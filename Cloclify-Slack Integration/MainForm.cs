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

        public MainForm(IStartupKeyManager startupKeyManager)
        {
            InitializeComponent();

            this.startupKeyManager = startupKeyManager;
            initApiKeyForm = new InitApiKeyForm();

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

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
