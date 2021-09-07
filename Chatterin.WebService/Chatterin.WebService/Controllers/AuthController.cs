using Chatterin.ClassLibrary;
using Chatterin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatterin.WebService.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

       
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ApiResult<IEnumerable<TokenDto>>>> Login([FromBody] LoginDto login)
        {
            var result = await _authenticationService.Authenticate(login.UserName, login.Password);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<ApiResult<IEnumerable<TokenDto>>>> RefreshTokens([FromBody] TokenDto refreshToken)
        {
            //get the JWT from the auth header
            var bearerToken = Request.Headers["Authorization"].ToString().Split()[1];

            //validate refresh token against that user
            var tokens = await _authenticationService.RefreshTokens(bearerToken, refreshToken.Token);

            if (!tokens.Success)
            {
                return BadRequest();
            }
            else
            {
                return Ok(tokens);
            }
        }
    }
}
