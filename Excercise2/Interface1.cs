using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2
{
    public interface IRestClient
    {
        IRestClient WithBaseUrl(string url);
        IRestClient WithBasicAuth(string username, string password);
        IRestClient WithHeader(string key, string value);
        IRestRequest CreateRequest(string path);
    }

    public interface IRestRequest
    {
        IRestRequest WithBasicAuth(string username, string password);
        IRestRequest WithHeader(string key, string value);
        Task<string> GetAsync();
    }
}
