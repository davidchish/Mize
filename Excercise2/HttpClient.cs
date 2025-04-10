using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2
{
    public class HttpClientRestClient : RestClientBase
    {
        public override IRestRequest CreateRequest(string path)
        {
            return new HttpClientRestRequest($"{_baseUrl}/{path.TrimStart('/')}", _authHeader, _headers);
        }
    }

    public class HttpClientRestRequest : IRestRequest
    {
        private readonly string _url;
        private string _authHeader;
        private readonly Dictionary<string, string> _headers = new();

        public HttpClientRestRequest(string url, string auth, Dictionary<string, string> headers)
        {
            _url = url;
            _authHeader = auth;
            foreach (var kv in headers)
                _headers[kv.Key] = kv.Value;
        }

        public IRestRequest WithBasicAuth(string username, string password)
        {
            var auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
            _authHeader = $"Basic {auth}";
            return this;
        }

        public IRestRequest WithHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        public async Task<string> GetAsync()
        {
            using var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, _url);
            if (!string.IsNullOrEmpty(_authHeader))
                req.Headers.Add("Authorization", _authHeader);
            foreach (var kv in _headers)
                req.Headers.Add(kv.Key, kv.Value);

            var res = await client.SendAsync(req);
            return await res.Content.ReadAsStringAsync();
        }
    }
}
