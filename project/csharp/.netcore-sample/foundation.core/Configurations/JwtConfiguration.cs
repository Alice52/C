namespace Edu.Ntu.Foundation.Core.Configurations
{

    /// <summary>
    ///  this class is for get token.
    /// </summary>
    public class JwtConfiguration
    {
        public string BaseUrl { get; set; }
        public int Timeout { get; set; }
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OAuthClientKey { get; set; }
        public string OAuthClientSecret { get; set; }
    }
}
