using CommandLineParser.Arguments;
using CommandLineParser.Validation;

namespace Sideline.Loadtest
{
    [ArgumentGroupCertification("u,a,t",EArgumentGroupCondition.ExactlyOneUsed)]
    internal class Settings
    {
        [ValueArgument(typeof(string), 'h',"host", Description="Api host", Optional = true, DefaultValue ="http://localhost:8080/su")]
        public string BaseUrl="";
        [ValueArgument(typeof(string), 's',"database", Description="Name of database", Optional = true, DefaultValue ="sua")]
        public string Database="";
        [ValueArgument(typeof(string), 'u', "user", Description = "Username")]
        public string Username="";
        [ValueArgument(typeof(string), 'p', "password", Description = "Password", Optional = true, DefaultValue = "undantag")]
        public string Password="";
        [ValueArgument(typeof(string), 't', "token", Description = "Facebook token")]
        public string Token = "";
        [ValueArgument(typeof(int), 'i', "iterations", Description = "Iterations", Optional = true, DefaultValue = 160)]
        public int Iterations = 0;
        [SwitchArgument('a',false, Description = "Ask for username at runtime")]
        public bool AskForUser=false;
        [SwitchArgument('w', false, Description = "Idle watcher only")]
        public bool IdleWatcher = false;
        [SwitchArgument('d', false, Description = "Delay start until match start.")]
        public bool Delay=false;
    }
}
