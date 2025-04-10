using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2
{
    public abstract class RestClientBase : IRestClient
    {
        protected string _baseUrl;
        protected string _authHeader;
        protected Dictionary<string, string> _headers = new();

        public IRestClient WithBaseUrl(string url)
        {
            _baseUrl = url.TrimEnd('/');
            return this;
        }

        public IRestClient WithBasicAuth(string username, string password)
        {
            var auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
            _authHeader = $"Basic {auth}";
            return this;
        }

        public IRestClient WithHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        public abstract IRestRequest CreateRequest(string path);
    }
}
