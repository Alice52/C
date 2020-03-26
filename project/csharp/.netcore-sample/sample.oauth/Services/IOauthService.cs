using Edu.Ntu.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.oauth.Services
{
    public interface IOauthService
    {
        JwtTokenModel GetAccessToken();
    }
}
