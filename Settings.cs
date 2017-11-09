using CommandLineParser.Arguments;
using CommandLineParser.Validation;

namespace Sideline.Loadtest
{
    [ArgumentGroupCertification("u,a",EArgumentGroupCondition.ExactlyOneUsed)]
    internal class Settings
    {
        [ValueArgument(typeof(string), 'h',"host", Description="Api host", Optional = true, DefaultValue ="http://localhost/")]
        public string BaseUrl="";
        [ValueArgument(typeof(string), 's',"database", Description="Name of database", Optional = true, DefaultValue ="sua")]
        public string Database="";
        [ValueArgument(typeof(string), 'u', "user", Description = "Username")]
        public string Username="";
        [ValueArgument(typeof(string), 'p', "password", Description = "Password", Optional = true, DefaultValue = "testare")]
        public string Password="";
        [ValueArgument(typeof(int), 'i', "iterations", Description = "Iterations", Optional = true, DefaultValue = 200)]
        public int Iterations = 0;
        [SwitchArgument('a',false, Description = "Ask for username at runtime")]
        public bool AskForUser=false;
        [ValueArgument(typeof(int),'d',"delay", Description = "Delay number of milliseconds between call", Optional =true,DefaultValue = 50)]
        public int DelayMult=0;
    }
}
