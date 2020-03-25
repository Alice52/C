namespace Edu.Ntu.Foundation.Core.Configurations
{
    /// <summary>
    ///  this class is for validate token.
    /// </summary>
    public class JwtBearerConfig
    {
        public string BaseUrl { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool RequireExpirationTime { get; set; }
    }
}
