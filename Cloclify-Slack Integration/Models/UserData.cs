using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Models
{
    public class UserData
    {
        public string UserName { get; set; }
        public List<Workspace> Workspaces { get; set; }
    }
}
