using Cloclify_Slack_Integration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Interfaces
{
    public interface IConfigManager
    {
        Config ReadConfig();
        void WriteConfig(Config config);
    }
}
