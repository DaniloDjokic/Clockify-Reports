using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration.Interfaces
{
    public interface IWebClient
    {
        void AddDefaultHeader(string headerName, string headerValue);
        Task<T> SendRequest<T>(string httpMethod, string endpoint, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, dynamic body = null);
    }
}
