using Chatterin.ClassLibrary;
using Chatterin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chatterin.WebService.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        IUserService _userService;
        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("user-name-available")]
        public async Task<ActionResult<ApiResult<bool>>> UserNameAvailable(string userName)
        {
            return Ok(await _userService.UserNameAvailable(userName));
        }

        [HttpPost]
        [Route("create-account")]
        public async Task<ActionResult<ApiResult<CreateAccountDto>>> CreateAccount(CreateAccountDto dto)
        {
            return Ok(await _userService.CreateUser(dto));
        }
    }
}
