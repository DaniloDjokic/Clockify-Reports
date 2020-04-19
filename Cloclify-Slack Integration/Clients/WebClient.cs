using Cloclify_Slack_Integration.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cloclify_Slack_Integration
{
    public class WebClient : IWebClient
    {
        private HttpClient httpClient;

        public WebClient(HttpClient httpClient, string endpoint)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(endpoint);
        }

        public void AddDefaultHeader(string headerName, string headerValue)
        {
            if(!this.httpClient.DefaultRequestHeaders.Contains(headerName))
                this.httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
        }

        public async Task<T> SendRequest<T>(string httpMethod, string endpoint, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, dynamic body = null)
        {
            endpoint = CreateRoute(endpoint, queryParams);

            HttpRequestMessage requestMessage = new HttpRequestMessage();

            requestMessage.Method = new HttpMethod(httpMethod);
            requestMessage.RequestUri = new Uri(httpClient.BaseAddress, endpoint);

            requestMessage = this.FormatMessage(requestMessage, headers, body);

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if(responseMessage.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());

            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }

        private string CreateRoute(string endpoint, Dictionary<string, string> queryParams)
        {            
            if (queryParams != null)
            {
                endpoint += '?';

                foreach (KeyValuePair<string, string> pair in queryParams)
                {
                    endpoint += pair.Key + "=" + pair.Value + "&";  
                }
            }

            return endpoint;
        }

        private HttpRequestMessage FormatMessage(HttpRequestMessage message, Dictionary<string, string> headers, dynamic body = null)
        {
            if (body != null)
                message.Content = body;

            if (headers != null)
            {
                foreach(KeyValuePair<string, string> pair in headers)
                {
                    message.Headers.Add(pair.Key, pair.Value);
                }
            }

            return message;
        }
    }
}
