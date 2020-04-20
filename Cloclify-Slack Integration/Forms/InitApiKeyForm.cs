using Cloclify_Slack_Integration.Interfaces;
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
        IInitLogicProvider logicProvider;

        public InitApiKeyForm(IInitLogicProvider logicProvider)
        {
            InitializeComponent();

            this.logicProvider = logicProvider;

            this.errorLabel.Visible = false;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SetErrorLabel(false);

            string apiKey = keyInputTextBox.Text;

            bool isApiKeyConfigured = await logicProvider.TryConfigureApiKey(apiKey);

            if (isApiKeyConfigured)
            { 
                this.Close();
            }
            else
            {
                Cursor.Current = Cursors.Default;
                this.SetErrorLabel(true);
            }
        }

        private void SetErrorLabel(bool set)
        {
            this.errorLabel.Visible = set;
        }

        private void InitApiKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!logicProvider.IsApiKeyConfigured)
                Application.Exit();
        }
    }
}
