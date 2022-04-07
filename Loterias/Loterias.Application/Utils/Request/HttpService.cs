using Loterias.Application.Utils.Request.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Loterias.Application.Utils.Request
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(string endpoint, Dictionary<string, string> requestParams) where T : class
        {
            try
            {
                string urlRequest = BuildUrlRequest(endpoint, requestParams);
                HttpResponseMessage response = await _httpClient.GetAsync(urlRequest);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch
            {
                throw;
            }
        }

        private string BuildUrlRequest(string endpoint, Dictionary<string, string> requestParams)
        {
            StringBuilder urlRequest = new StringBuilder();

            foreach (var requestParam in requestParams)
            {
                urlRequest.Append(endpoint).Append('?').Append(requestParam.Key).Append('=').Append(requestParam.Value);
            }

            return urlRequest.ToString();
        }
    }
}
