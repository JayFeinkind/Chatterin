using Chatterin.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public interface IUserService
    {
        Task<ApiResult<bool>> UserNameAvailable(string userName);
        Task<ApiResult<CreateAccountDto>> CreateUser(CreateAccountDto dto);
        Task<ApiResult<IEnumerable<UserDto>>> GetUsers();
    }
}
