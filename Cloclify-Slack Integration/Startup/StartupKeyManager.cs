using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cloclify_Slack_Integration
{
    public class StartupKeyManager : IStartupKeyManager
    {
        private string fileName = "Key.dat";

        private string apiKey;
        public string ApiKey { get { return this.apiKey; } }

        public StartupKeyManager() { }

        public bool InitializeApiKey()
        {
            try
            {
                using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.OpenOrCreate)))
                {
                    apiKey = br.ReadString();
                }

                if (apiKey == null)
                    return false;

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool WriteApiKey(string apiKey)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
                {
                    bw.Write(apiKey);
                    this.apiKey = apiKey;
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
