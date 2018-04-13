using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sideline.Loadtest
{
    internal class GenericAPI
    {
        private readonly string _baseUrl;
        private System.Net.CookieContainer _cc = new System.Net.CookieContainer();
        protected string SessionIdDescriptior = "";
        public GenericAPI(string baseUrl)
        {
            _baseUrl = baseUrl;
            if (!_baseUrl.EndsWith("/")) _baseUrl += "/";
        }
        protected async Task<T> Call<T>(string endpoint)
        {
            var res = await GetData(endpoint);
            var nisse = default(T);

            try
            {
                nisse = JsonConvert.DeserializeObject<T>(res);
            }
            catch (Exception)
            {
                Console.WriteLine(res);
            }
            return nisse;
        }
        protected async Task<string> GetData(string method)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = _cc;
            handler.UseCookies = true;
            handler.UseDefaultCredentials = true;
            handler.UseProxy = true;

            using (HttpClient client = new HttpClient(handler))
            {
                if (SessionIdDescriptior != "")
                {
                    if (method.Contains("?"))
                    {

                        method += "&";
                    }
                    else
                    {
                        method += "?";
                    }
                    method += SessionIdDescriptior;
                }
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = client.GetAsync(method).Result;
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }
    }
}