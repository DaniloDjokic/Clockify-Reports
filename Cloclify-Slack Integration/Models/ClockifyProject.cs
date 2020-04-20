using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration
{
    public class ClockifyProject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ClockifyTask> Tasks { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
