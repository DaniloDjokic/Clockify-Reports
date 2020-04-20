using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration
{
    public class TimeEntry
    {
        public string TaskId { get; set; }
        public string ProjectId { get; set; }
        public string Description { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }

    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Duration { get; set; }
    }
}
