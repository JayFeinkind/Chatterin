using System;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;

namespace Chatterin.Services
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<string>> Login(string username, string password);
    }
}
