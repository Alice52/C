using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edu.Ntu.Foundation.Core.Models;
using Edu.Ntu.Oauth.Clients;
using Microsoft.Extensions.Logging;

namespace sample.oauth.Services
{
    public class OauthService : IOauthService
    {
        private readonly ILogger<IOauthService> _logger;

        private readonly IJwtTokenClient _jwtTokenClient;

        public OauthService(ILogger<IOauthService> logger, IJwtTokenClient jwtTokenClient)
        {
            _logger = logger;
            _jwtTokenClient = jwtTokenClient;
        }

        public JwtTokenModel GetAccessToken()
        {
            return _jwtTokenClient.getAccessToken();
        }
    }
}
