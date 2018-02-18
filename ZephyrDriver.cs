using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using ZephyrHelper.Entities;
using ZephyrHelper.Extensions;
using Version = ZephyrHelper.Entities.Version;

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
        public void UpdateExecutionStatus(int exucutionId, Enums.ExecutionStatus status)
        {
            var putRequest = $@"{jiraBaseUrl}/rest/zapi/latest/execution/{exucutionId}/execute";
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

        public string GetCycleIdByName(string projectId, string cycleName)
        {
            var url = $@"{jiraBaseUrl}/rest/zapi/latest/cycle?projectId={projectId}&versionId=-1";
            var resp = GetRequest(url);
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp);
            var cycles = new Dictionary<string,CyclePreview>();
            foreach (var val in values)
            {
                if(val.Key !="recordsCount")
                    cycles.Add(val.Key,JsonConvert.DeserializeObject<CyclePreview>(val.Value.ToString()));
            }
            return cycles.First(c => c.Value.Name == cycleName).Key;
        }

        /// <summary>
        /// Return all cycles that assigned to version
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="versionId"></param>
        /// <returns>key is a cycle Id</returns>
        public Dictionary<string, CyclePreview> GetCycles(string projectId, string versionId)
        {
            var url = $@"{jiraBaseUrl}/rest/zapi/latest/cycle?projectId={projectId}&versionId={versionId}";
            var resp = GetRequest(url);
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp);
            var cycles = new Dictionary<string, CyclePreview>();
            foreach (var val in values)
            {
                if (val.Key != "recordsCount")
                    cycles.Add(val.Key, JsonConvert.DeserializeObject<CyclePreview>(val.Value.ToString()));
            }
            return cycles;
        }

        public List<Version> GeVersions(string projectIdOrKey)
        {
            var url = $@"{jiraBaseUrl}/rest/api/2/project/{projectIdOrKey}/versions";
            var resp = GetRequest(url);
            return JsonConvert.DeserializeObject<List<Version>>(resp);
        }

        public CycleInfo GetCycleInfoById(string projectId, string cycleId)
        {
            var url = $@"{jiraBaseUrl}/rest/zapi/latest/cycle/{cycleId}";
            return JsonConvert.DeserializeObject<CycleInfo>(GetRequest(url));
        }
        public List<ExecutionInfo> GetCycleExecutions(string cycleId)
        {
            var url = $@"{jiraBaseUrl}/rest/zapi/latest/execution?cycleId={cycleId}";
            var response = GetRequest(url);
            var res = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            return JsonConvert.DeserializeObject<List<ExecutionInfo>>(res["executions"].ToString());
        }

        public ProjectInfo GetProjectInfo(string projectName)
        {
            var url = $@"{jiraBaseUrl}/rest/api/2/project/{projectName}";
            var response = GetRequest(url);
            return JsonConvert.DeserializeObject<ProjectInfo>(response);
        }

        public string GetIssueIdByKey(string issueKey)
        {
            var url = $@"{jiraBaseUrl}/rest/api/2/issue/{issueKey}";
            var response = GetRequest(url);
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            return result["id"].ToString();
        }
        public void AttachFileToExecution(int executionId, string pathToFile)
        {
            var restClient = new RestClient(jiraBaseUrl);
            {
                var requestattachment = new RestRequest(Method.POST);
                requestattachment.Resource = $"/rest/zapi/latest/attachment?entityId={executionId}&entityType=execution";
                requestattachment.RequestFormat = DataFormat.Json;
                requestattachment.AddHeader("X-Atlassian-Token", "no-check");
                requestattachment.AddHeader("Content-Type", "application/json");
                requestattachment.AddHeader("Authorization", "Basic " + encodingAuth);
                requestattachment.AddFile("file", pathToFile, "multipart/form-data");

                IRestResponse responseattachment = restClient.Execute(requestattachment);
                Console.Write(responseattachment);
                //ToDo check for successfull result
            }
        }
        #endregion
    }
}
