using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2
{
    public static class RestClientFactory
    {
        public static IRestClient CreateDefault() => CreateHttpClient();

        public static IRestClient CreateHttpClient() => new HttpClientRestClient();

        public static IRestClient CreateWebRequest() => new WebRequestRestClient();
    }

}
