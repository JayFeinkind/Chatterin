using Chatterin.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public interface IAuthenticationService
    {
        Task<ApiResult<IEnumerable<TokenDto>>> Authenticate(string username, string password);
        Task<ApiResult<IEnumerable<TokenDto>>> RefreshTokens(string bearerToken, string refreshToken);
    }
}
