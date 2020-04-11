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
        HttpClient c = new HttpClient();

        public InitApiKeyForm()
        {
            InitializeComponent();
            c.BaseAddress = new Uri("https://api.clockify.me/api/v1/");
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private async void confirmButton_Click(object sender, EventArgs e)
        {
            string apiKey = keyInputTextBox.Text;

            c.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            HttpResponseMessage m = await c.GetAsync("user");

            string res = await m.Content.ReadAsStringAsync();
        }
    }
}
