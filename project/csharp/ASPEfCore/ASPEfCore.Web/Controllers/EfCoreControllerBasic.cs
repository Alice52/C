using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using AspEfCore.Data.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using RestSharp;
using RestSharp.Authenticators;

namespace AspEfCore.Web.Controllers
{
    [Route("api/EfCoreControllerBasic")]
    [ApiController]
    public class EfCoreControllerBasic : ControllerBase
    {
        private readonly CampDbContext _compDbContext;
        private readonly IBackgroundJobClient _backgroungJobClient;

        public EfCoreControllerBasic(CampDbContext compDbContext, IBackgroundJobClient backgroungJobClient)
        {
            // Inject
            _compDbContext = compDbContext;
            _backgroungJobClient = backgroungJobClient;
        }




        [HttpPut("testlong")]
        [SwaggerResponse((int)HttpStatusCode.OK, "deleteRelations succuessfully.")]
        public ActionResult testlong()
        {
            return Ok();
        }


        [HttpPost("api/oauth")]
        [SwaggerResponse((int)HttpStatusCode.OK, "deleteRelations succuessfully.")]
        public ActionResult oauth()
        {
            var client = new RestClient("https://101.132.45.28:9443/oauth2/token");
            var request = new RestRequest(Method.POST);
            //request.AddParameter("client_id", "abcqaaadwqfwqfdsved");
            //request.AddParameter("client_secret", "abcqaaadwqfwqfdsved");

            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            client.Authenticator = new HttpBasicAuthenticator("abcqaaadwqfwqfdsved", "abcqaaadwqfwqfdsved");
            //request.AddHeader("accept", "application/xml");
            //request.AddHeader("authorization", "Basic VFNUMTpiYXJ5YXY=");
            request.AddParameter("application/x-www-form-urlencoded",
                "grant_type=password&username=admin&password=admin",
                ParameterType.RequestBody);
            client.RemoteCertificateValidationCallback = (a, b, c,d)=> true;
            IRestResponse response = client.Execute(request);

            return Ok();
        }

         
    }
}
