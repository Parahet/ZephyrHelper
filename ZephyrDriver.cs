using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZephyrHelper.Extensions;

namespace ZephyrHelper
{
    public class ZephyrDriver
    {
        private string jiraBaseUrl;
        private Authorization authorization;
        private string encodingAuth;

        public ZephyrDriver(string jiraBaseUrl, Authorization auth)
        {
            this.jiraBaseUrl = jiraBaseUrl;
            this.authorization = auth;
            encodingAuth = authorization.GetEncoding();
        }

        private string GetRequest(string url)
        {
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.ContentType = "application/json";
            request2.Accept = "application/json";
            request2.Method = "Get";
            request2.Headers[HttpRequestHeader.Authorization] = "Basic " + encodingAuth;

            var wresp = (HttpWebResponse)request2.GetResponse();
            using (Stream stream = wresp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        #region Public Methods
        public void UpdateExecutionStatus(string exucutionId, Enums.ExecutionStatus status)
        {
            var putRequest = $@"https://jira.effective-soft.com/rest/zapi/latest/execution/{exucutionId}/execute";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + encodingAuth;

                var responseStr = client.UploadString(putRequest, "PUT", status.ToText());
                Console.Write(responseStr);
                //ToDo: Check for response
            }
        }
        #endregion
    }
}
