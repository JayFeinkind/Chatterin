using Chatterin.ClassLibrary;
using Chatterin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatterin.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController(IUserService userService) : AuthorizedController
    {
        [HttpGet]
        [Route("Users")]
        public async Task<ActionResult<ApiResult<IEnumerable<UserDto>>>> Users()
        {
            return Ok(await userService.GetUsers());
        }
    }
}
