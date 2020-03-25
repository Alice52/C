using System.Text;
using Edu.Ntu.Foundation.Core.Configurations;
using Edu.Ntu.Foundation.Core.Constants;
using Edu.Ntu.Foundation.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Edu.Ntu.Oauth.Clients
{
    public class JwtTokenClient : IJwtTokenClient
    {
        private const string Path = "/oauth2/token";
        private readonly string _baseUrl;
        private readonly int _timeout;
        private readonly string _grantType;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _oAuthClientKey;
        private readonly string _oAuthClientSecret;
        private readonly ILogger<IJwtTokenClient> _logger;

        public JwtTokenClient(ILogger<JwtTokenClient> logger,
            IOptions<JwtConfiguration> config)
        {
            _logger = logger;
            _baseUrl = config.Value.BaseUrl;
            _timeout = config.Value.Timeout;
            _grantType = config.Value.GrantType;
            _userName = config.Value.UserName;
            _password = config.Value.Password;
            _oAuthClientKey = config.Value.OAuthClientKey;
            _oAuthClientSecret = config.Value.OAuthClientSecret;
        }
        public JwtTokenModel getAccessToken()
        {
            var request = new RestRequest(Path, Method.POST) { Timeout = 10000 };
            request.AddHeader(RequestHeaders.CONTENT_TYPE, RequestHeaders.FORM_CONTENT);
            StringBuilder sb = new StringBuilder();
            sb.Append("grant_type=").Append(_grantType)
                .Append("&username=").Append(_userName)
                .Append("&password=").Append(_password);
            var client = new RestClient(_baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(_oAuthClientKey, _oAuthClientSecret);
            request.AddParameter(RequestHeaders.FORM_CONTENT, sb.ToString(), ParameterType.RequestBody);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<JwtTokenModel>(response.Content);
            }
            else
            {
                _logger.LogWarning("Get token fialed from wso2 is, cause by: " + response.ErrorException.StackTrace);
            }

            return null;
        }
    }
}