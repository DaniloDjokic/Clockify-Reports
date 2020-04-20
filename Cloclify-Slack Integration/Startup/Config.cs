using Cloclify_Slack_Integration.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Startup
{
    public class Config
    {
        public Workspace Workspace { get; set; }
        public string SaveFolderPath { get; set; }
        public List<ClockifyProject> Projects { get; set; }
    }

    public class ConfigManager : IConfigManager
    {
        private string fileName = "Config.dat";

        public Config ReadConfig()
        {
            try
            {
                using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.OpenOrCreate)))
                {
                    Config config = JsonConvert.DeserializeObject<Config>(br.ReadString());
                    return config;
                }
            }
            catch(Exception e)
            {
                return new Config { Projects = new List<ClockifyProject>(), SaveFolderPath = "" };
            }
        }

        public void WriteConfig(Config config)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            {
                string json = JsonConvert.SerializeObject(config);
                bw.Write(json);
            }
        }
    }
}
