using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2
{
    public class WebRequestRestClient : RestClientBase
    {
        public override IRestRequest CreateRequest(string path)
        {
            return new WebRequestRestRequest($"{_baseUrl}/{path.TrimStart('/')}", _authHeader, _headers);
        }
    }

    public class WebRequestRestRequest : IRestRequest
    {
        private readonly string _url;
        private string _authHeader;
        private readonly Dictionary<string, string> _headers = new();

        public WebRequestRestRequest(string url, string auth, Dictionary<string, string> headers)
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
            var request = WebRequest.CreateHttp(_url);
            request.Method = "GET";
            if (!string.IsNullOrEmpty(_authHeader))
                request.Headers["Authorization"] = _authHeader;
            foreach (var kv in _headers)
                request.Headers[kv.Key] = kv.Value;

            using var response = await request.GetResponseAsync();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
