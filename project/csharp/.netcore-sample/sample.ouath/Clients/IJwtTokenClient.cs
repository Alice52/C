using Edu.Ntu.Foundation.Core.Models;

namespace Edu.Ntu.Oauth.Clients
{
    public interface IJwtTokenClient
    {
        JwtTokenModel getAccessToken();
    }
}