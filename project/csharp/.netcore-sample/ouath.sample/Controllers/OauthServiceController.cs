using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Augmentum.TableOperation.TableLifecycle.Controllers
{
    [Route("/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class OauthServiceController : ControllerBase
    {

        public OauthServiceController()
        {
            
        }

        [HttpGet()]
        public void GetAccessToken()
        {
            
        }
    }
}