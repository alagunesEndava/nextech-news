using System.Net;
using System;
using System.Threading.Tasks;
using RestSharp;

namespace nextech.news.Core.Utils
{
    public class RestClientUtil
    {
        private readonly RestClient _restClient;
        private const string baseUrl = $"https://hacker-news.firebaseio.com/v0";


        public RestClientUtil(RestClient restClient)
        {
            this._restClient = restClient;

        }

        public async Task<T> GetResourceAsync<T>(string resource)
        {
            var request = new RestRequest
            {
                Resource = $"{baseUrl}{resource}",
                Method = Method.Get
            };
            var response = await this._restClient.ExecuteAsync<T>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }

            throw new Exception($"Failed to get resource. Status code: {response.StatusCode}");
        }

    }
}
