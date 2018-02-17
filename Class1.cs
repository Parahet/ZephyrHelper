using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Xunit;

namespace ZephyrHelper
{
    public class Class1
    {
        [Fact]
        public void test01()
        {
            string responseData;
            /*static async Task GetListOfExecution()
            {
                var baseAddress = new Uri("http://jira.effective-soft.com/rest/zapi/latest/cycle?projectId=10000&versionId=10100");
    
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
    
    
                    using (var response = await httpClient.GetAsync("undefined"))
                    {
    
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                }
            }*/

            string jiraBaseUrl = "http://jira.effective-soft.com";
            string userName = "Eugene.Parakhonko";
            string password = "C123456asd";
            //private static string projectKey = "CU584";
            var urlString =
                "https://jira.effective-soft.com/flex/services/rest/latest/execution/8"; //8 is the schedule id 
            var issueKey = "CU584-2965";
            var issueId = "426183";
            var projectId = "12931";
            var projectKey = "CU584";
            string cycle = "Auto test set";
            var cycleId = "32";

            var getExecutionIds = $@"https://jira.effective-soft.com/rest/zapi/latest/execution?issueId={issueId}";
            var getCysles = $@"https://jira.effective-soft.com/rest/zapi/latest/cycle?projectId={projectId}";
            var getProject = $@"https://jira.effective-soft.com/rest/api/2/project/{projectId}";
            var getIssueId = $@"https://jira.effective-soft.com/rest/api/2/issue/{issueKey}";
            var getCysleIds = $@"https://jira.effective-soft.com/rest/zapi/latest/cycle?projectId={projectId}";
            var getCycleInfo = $@"https://jira.effective-soft.com/rest/zapi/latest/cycle/{cycleId}";
            var getListOfFolders =
                $@"https://jira.effective-soft.com/rest/zapi/latest/cycle/{
                        cycleId
                    }/folders?projectId={projectId}"; // not found
            var getCycleCases = $@"https://jira.effective-soft.com/rest/zapi/latest/execution?cycleId={cycleId}";


            String userPassword = "eugene.parakhonko:C123456asd";
            //static String payload="{\"lastTestResult\":[{ \"executionStatus\": \"2\"}]}";
            var encoding = Convert.ToBase64String(Encoding.UTF8.GetBytes(userPassword));

            var url = new Uri(urlString);

            /*	HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(getExecutionIds);
                request2.Timeout = 10000;
                request2.ReadWriteTimeout = 10000;
                request2.ContentType = "application/json";
                request2.Accept = "application/json";
                request2.Method = "Get";
                request2.Headers[HttpRequestHeader.Authorization] = "Basic " + encoding;

                var wresp = (HttpWebResponse)request2.GetResponse();
                using (Stream stream = wresp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString2 = reader.ReadToEnd();
                    Console.Write(responseString2);
                }*/

            String payload = "{\"lastTestResult\":{ \"executionStatus\": \"3\"}}"; // 2 = fail, 1= pass and 3= WIP 
            String statusPass = "{\"status\":\"1\"}"; // 2 = fail, 1= pass and 3= WIP 
            String statusFail = "{\"status\":\"2\"}";
            String statusWIP = "{\"status\":\"3\"}";
            var exId1 = "318";
            var exId2 = "319";
            var exId3 = "320";
            var putReq1 = $@"https://jira.effective-soft.com/rest/zapi/latest/execution/{exId1}/execute";
            var putReq2 = $@"https://jira.effective-soft.com/rest/zapi/latest/execution/{exId2}/execute";
            var putReq3 = $@"https://jira.effective-soft.com/rest/zapi/latest/execution/{exId3}/execute";

            /*	
             * //Update execution status
             *	using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + encoding;

                    var responseStr = client.UploadString(putReq1, "PUT", statusPass);
                    Console.Write(responseStr);
                }
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + encoding;

                    var responseStr = client.UploadString(putReq2, "PUT", statusFail);
                    Console.Write(responseStr);
                }
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + encoding;

                    var responseStr = client.UploadString(putReq3, "PUT", statusWIP);
                    Console.Write(responseStr);
                }*/
            var path = "D:\\SearchMarket12884.Jpeg";
            var postReq =
                $@"https://jira.effective-soft.com/rest/zapi/latest/attachment?entityId={exId2}&entityType=execution";

            //updload file to execution
            /*using (var client = new WebClient())
			{
				var files = "{'file':(SearchMarket12884.Jpeg, "+File.ReadAllBytes(path)+", \"multipart/form-data\")}";
				client.Headers[HttpRequestHeader.ContentType] = "application/json";
				client.Headers[HttpRequestHeader.Accept] = "application/json";
				client.Headers[HttpRequestHeader.Authorization] = "Basic " + encoding;
				client.Headers.Add("X-Atlassian-Token", "nocheck");

				var responseStr = client.UploadString(postReq, "post", files);
				Console.Write(responseStr);
			}**/

            var restClient = new RestClient("https://jira.effective-soft.com");
            {
                var bb = File.ReadAllBytes(path);
                var img = Image.FromFile(path);
                var requestattachment = new RestRequest(Method.POST);
                requestattachment.Resource = $"/rest/zapi/latest/attachment?entityId={exId2}&entityType=execution";
                requestattachment.RequestFormat = DataFormat.Json;
                requestattachment.AddHeader("X-Atlassian-Token", "no-check");
                requestattachment.AddHeader("Content-Type", "application/json");
                requestattachment.AddHeader("Authorization", "Basic " + encoding);
                requestattachment.AddFile("file", path, "multipart/form-data");

                IRestResponse responseattachment = restClient.Execute(requestattachment);
                Console.Write(responseattachment);
            }

        }
    }
}
