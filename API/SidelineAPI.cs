using System.Threading.Tasks;
using Sideline.Loadtest.viewmodels;

namespace Sideline.Loadtest
{
    internal class SidelineAPI : GenericAPI
    {

        public SidelineAPI(string baseUrl) : base(baseUrl)
        {
        }
        
        internal async Task<Stats> Stats()
        {
            return await Call<Stats>("stats.php");
        }

        internal async Task<Joinmatch> JoinMatch()
        {
            return await Call<Joinmatch>("joinmatch.php");
        }

        internal async Task<string> Run(int count, int slot)
        {
            return await GetData($"run.php?times={count}&slot={slot}");
        }
        internal async Task<string> Coin(int count, int slot)
        {
            return await GetData($"coin.php?times={count}&slot={slot}");
        }

        internal async Task<Matchstate> MatchState()
        {
            return await Call<Matchstate>("matchstate.php");
        }

        internal async Task<Playerinfo> Player()
        {
            return await Call<Playerinfo>("player.php");
        }

        internal async Task<LoginResultObject> Login(string userName, string password, string dbname)
        {
            var res = await Call<LoginResultObject>($"login.php?db={dbname}&usr={userName}&pw={password}");
            if(res != null) SessionIdDescriptior = res.SessionId;

            return res;
        }

        internal async Task<LoginResultObject> FacebookLogin(string token, string dbname)
        {
            var res = await Call<LoginResultObject>($"loginfacebook2.php?db={dbname}&token={token}");
            if (res != null) SessionIdDescriptior = res.SessionId;

            return res;
        }


    }
}