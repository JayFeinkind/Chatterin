using Chatterin.ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatterin.WebService.Controllers
{
    public abstract class AuthorizedController : ControllerBase
    {
        protected int GetUserIdFromClaims()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var userClaim = claims.FindFirst(Codes.UserId);

            int.TryParse(userClaim?.Value, out int result);

            return result;
        }
    }
}
