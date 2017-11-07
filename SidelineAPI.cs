using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sideline.Loadtest
{
    internal class SidelineAPI
    {
        private readonly string _baseUrl;
        private System.Net.CookieContainer _cc = new System.Net.CookieContainer();

        public SidelineAPI(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        internal async Task<string> Stats()
        {
            return await GetData("/stats.php", "", "");
        }

        internal async Task<string> JoinMatch()
        {
            return await GetData("/joinmatch.php", "", "");
        }

        internal async Task<string> Run(int count, int slot)
        {
            return await GetData($"/run.php?times={count}&slot={slot}", "", "");
        }
        internal async Task<string> Coin(int count, int slot)
        {
            return await GetData($"/coin.php?times={count}&slot={slot}", "", "");
        }

        internal async Task<string> MatchState()
        {
            return await GetData($"/matchstate.php", "", "");
        }

        internal async Task<string> Login(string userName, string password)
        {
            return await GetData("/login.php?db=sideline",userName,password);
        }

        private async Task<string> GetData(string method, string user, string password)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = _cc;
            handler.UseCookies = true;
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                if (user != "")
                {
                    string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(user + ":" + password));
                    client.DefaultRequestHeaders.Add("Authorization", auth);
                }
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = client.GetAsync(method).Result;
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
        }
    }
}