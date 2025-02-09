﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;

namespace AsyncEnumerableApp2
{
    public class GraphQLRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("variables")]
        public IDictionary<string, object> Variables { get; } = new Dictionary<string, object>();

        public string ToJsonText() =>
            JsonConvert.SerializeObject(this);
    }

    class Program
    {
        public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        public static async Task<IEnumerable<int>> GenerateSequence2()
        {
            var list = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                list.Add(i);
            }

            return list.ToArray();
        }


        static async Task Main(string[] args)
        {
            await foreach (var item in GenerateSequence())
            {
                Console.WriteLine(item);
            }

            await using (var memoryStream = new MemoryStream()) ;


            //Follow these steps to create a GitHub Access Token
            // https://help.github.com/articles/creating-a-personal-access-token-for-the-command-line/#creating-a-token
            //Select the following permissions for your GitHub Access Token:
            // - repo:status
            // - public_repo
            // Replace the 3rd parameter to the following code with your GitHub access token.
            var key = "701a17ebefe0f037fb4be0896f6b6442d47dec12";

            var client = new GitHubClient(new Octokit.ProductHeaderValue("IssueQueryDemo"))
            {
                Credentials = new Octokit.Credentials(key)
            };

            var progressReporter = new progressStatus((num) =>
            {
                Console.WriteLine($"Received {num} issues in total");
            });
            CancellationTokenSource cancellationSource = new CancellationTokenSource();


            try
            {
                //var results = await runPagedQueryAsync(client, PagedIssueQuery, "docs",
                //    cancellationSource.Token, progressReporter);
                //foreach (var issue in results)
                //    Console.WriteLine(issue);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Work has been cancelled");
            }


            try
            {
                var results = runPagedQueryAsync(client, PagedIssueQuery, "docs");
                await foreach (var issue in results.WithCancellation(cancellationSource.Token))
                    Console.WriteLine(issue);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Work has been cancelled");
            }
        }

        private const string PagedIssueQuery =
            @"query ($repo_name: String!,  $start_cursor:String) {
  repository(owner: ""dotnet"", name: $repo_name) {
    issues(last: 25, before: $start_cursor)
     {
        totalCount
        pageInfo {
          hasPreviousPage
          startCursor
        }
        nodes {
          title
          number
          createdAt
        }
      }
    }
  }
";

        private static async IAsyncEnumerable<JToken> runPagedQueryAsync(GitHubClient client,
            string queryText, string repoName)
        {
            var issueAndPRQuery = new GraphQLRequest
            {
                Query = queryText
            };
            issueAndPRQuery.Variables["repo_name"] = repoName;

            bool hasMorePages = true;
            int pagesReturned = 0;
            int issuesReturned = 0;

            // Stop with 10 pages, because these are large repos:
            while (hasMorePages && (pagesReturned++ < 10))
            {
                var postBody = issueAndPRQuery.ToJsonText();
                var response = await client.Connection.Post<string>(new Uri("https://api.github.com/graphql"),
                    postBody, "application/json", "application/json");

                JObject results = JObject.Parse(response.HttpResponse.Body.ToString());

                int totalCount = (int)issues(results)["totalCount"];
                hasMorePages = (bool)pageInfo(results)["hasPreviousPage"];
                issueAndPRQuery.Variables["start_cursor"] = pageInfo(results)["startCursor"].ToString();
                issuesReturned += issues(results)["nodes"].Count();

                foreach (JObject issue in issues(results)["nodes"])
                    yield return issue;
            }

            JObject issues(JObject result) => (JObject)result["data"]["repository"]["issues"];
            JObject pageInfo(JObject result) => (JObject)issues(result)["pageInfo"];
        }

        private class progressStatus : IProgress<int>
        {
            Action<int> action;
            public progressStatus(Action<int> progressAction) =>
                action = progressAction;

            public void Report(int value) => action(value);
        }

        private static async Task<JArray> runPagedQueryAsync(GitHubClient client, string queryText, string repoName, CancellationToken cancel, IProgress<int> progress)
        {
            var issueAndPRQuery = new GraphQLRequest
            {
                Query = queryText
            };
            issueAndPRQuery.Variables["repo_name"] = repoName;

            JArray finalResults = new JArray();
            bool hasMorePages = true;
            int pagesReturned = 0;
            int issuesReturned = 0;

            // Stop with 10 pages, because these are large repos:
            while (hasMorePages && (pagesReturned++ < 10))
            {
                var postBody = issueAndPRQuery.ToJsonText();
                var response = await client.Connection.Post<string>(new Uri("https://api.github.com/graphql"),
                    postBody, "application/json", "application/json");

                JObject results = JObject.Parse(response.HttpResponse.Body.ToString());

                int totalCount = (int)issues(results)["totalCount"];
                hasMorePages = (bool)pageInfo(results)["hasPreviousPage"];
                issueAndPRQuery.Variables["start_cursor"] = pageInfo(results)["startCursor"].ToString();
                issuesReturned += issues(results)["nodes"].Count();
                finalResults.Merge(issues(results)["nodes"]);
                progress?.Report(issuesReturned);
                cancel.ThrowIfCancellationRequested();
            }
            return finalResults;

            JObject issues(JObject result) => (JObject)result["data"]["repository"]["issues"];
            JObject pageInfo(JObject result) => (JObject)issues(result)["pageInfo"];
        }
    }
}
