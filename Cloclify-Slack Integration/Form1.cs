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
    public partial class Form1 : Form
    {
        HttpClient httpClient = new HttpClient();
        string apiKey = "XoimXJLmQw48XjqM";

        public Form1()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("https://api.clockify.me/api/v1");
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            httpClient.GetAsync("")
        }
    }
}
