using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;

namespace Chatterin.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        ISettingsService _settingsService;
        ApiService _apiService;

        public AuthenticationService(ApiService apiService,  ISettingsService settingsService) 
        {
            _settingsService = settingsService;
            _apiService = apiService;
        }

        /// <summary>
        /// Login and store tokens for future api calls
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> Login(string username, string password)
        {
            var result = new ServiceResult<string>();
            var url = "Auth/login";
            var model = new LoginDto
            {
                UserName = username,
                Password = password
            };

            var apiResult = await _apiService.PostAsync<LoginDto, IEnumerable<TokenDto>>(url, model, false);

            if (apiResult.Success)
            {
                _settingsService.SetLoginValues(username, apiResult?.Data?.Result);
                result.Success = true;
            }
            else
            {
                result.Error = apiResult.Data?.Errors?.FirstOrDefault();
            }

            return result;
        }
    }
}
