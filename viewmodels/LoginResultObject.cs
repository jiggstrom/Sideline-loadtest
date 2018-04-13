namespace Sideline.Loadtest
{
    public class LoginResultObject
    {
        public int PlayerId { get; set; }
        public string SessionId { get; set; }
        public string StatusInfo { get; set; }
        public bool VersionExpired { get; set; }
    }
}