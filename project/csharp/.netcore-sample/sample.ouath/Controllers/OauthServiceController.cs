using Edu.Ntu.Foundation.Core.Constants;
using Edu.Ntu.Foundation.Core.Models;
using Edu.Ntu.Foundation.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sample.ouath.Services;
using System;
using System.Net.Http;

namespace Augmentum.sample.ouath.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class OauthServiceController : ControllerBase
    {
        private readonly ILogger<OauthServiceController> _logger;

        private readonly IOauthService _oauthService;
        private readonly IHttpClientFactory _clientFactory;

        public OauthServiceController(ILogger<OauthServiceController> logger, IOauthService oauthService, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _oauthService = oauthService;
            _clientFactory = clientFactory;
        }

        [HttpPost()]
        public JwtTokenModel GetAccessToken()
        {
            var token = _oauthService.GetAccessToken();
            return token;
        }


        [HttpPost()]
        [Authorize]
        public void GetAccessToken2()
        {
            Console.WriteLine("This is aauthorize method.");
        }

        [HttpGet()]
        public void ValidateToken()
        {
            HttpUtil.Post<JwtTokenModel>(_clientFactory, ClientsConstants.TABLE_SERVICE_CLIENT
                    , null, "OauthService/GetAccessToken2");
        }
    }
}
