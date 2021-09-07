using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Chatterin.ClassLibrary;

namespace Chatterin.Services
{
    public class ApiService
    {
        private const string DefaultDomain = "http://10.211.55.3/Chatterin.WebService/";
        ISettingsService _settingsService;

        public ApiService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public string ConstructUrl(string appendage)
        {
            return DefaultDomain + appendage;
        }

        #region Api Logic

        public async Task<ApiResult<T>> GetAsync<T>(string url, bool addAuthorization = true)
        {
            return await GetData<T>(url, addAuthorization);
        }

        private async Task<ApiResult<T>> GetData<T>(string url, bool addAuthorization, bool isRetryAttempt = false) 
        {
            var result = new ApiResult<T>() { Success = false };

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, ConstructUrl(url));

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 120);

                    if (addAuthorization)
                    {
                        if (string.IsNullOrWhiteSpace(_settingsService.JwtToken))
                            throw new InvalidOperationException("Cannot add authorization. Jwt token is null");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settingsService.JwtToken);
                    }

                    var response = await client.SendAsync(requestMessage);

                    if (response != null)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrWhiteSpace(json) == false)
                        {
                            result.Data = (ApiResultData<T>)JsonConvert.DeserializeObject(json, typeof(ApiResultData<T>));
                        }

                        result.ResultCode = response.StatusCode;

                        if (response.StatusCode == HttpStatusCode.Unauthorized && addAuthorization && !isRetryAttempt)
                        {
                            var refreshSuccess = await RefreshTokens();

                            if (refreshSuccess)
                            {
                                return await GetData<T>(url, addAuthorization, !isRetryAttempt);
                            }
                        }

                        result.Success = response.StatusCode == HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Exception = ex;
            }

            return result;
        }

        public async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data, bool addAuthorization = true)
        {
            return await PostData<TRequest, TResponse>(url, data, addAuthorization);
        }

        private async Task<ApiResult<TResponse>> PostData<TRequest, TResponse>(string url, TRequest data, bool addAuthorization, bool isRetryAttempt = false)
        {
            var result = new ApiResult<TResponse>() { Success = false };

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, ConstructUrl(url));

                string json = JsonConvert.SerializeObject(data);

                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 120);

                    if (addAuthorization)
                    {
                        if (string.IsNullOrWhiteSpace(_settingsService.JwtToken))
                            throw new InvalidOperationException("Cannot add authorization. Jwt token is null");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settingsService.JwtToken);
                    }

                    var response = await client.SendAsync(requestMessage);

                    if (response != null)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrWhiteSpace(jsonResponse) == false)
                        {
                            result.Data = (ApiResultData<TResponse>)JsonConvert.DeserializeObject(jsonResponse, typeof(ApiResultData<TResponse>));
                        }

                        result.ResultCode = response.StatusCode;

                        if (response.StatusCode == HttpStatusCode.Unauthorized && addAuthorization && !isRetryAttempt)
                        {
                            var refreshSuccess = await RefreshTokens();

                            if (refreshSuccess)
                            {
                                return await PostData<TRequest, TResponse>(url, data, addAuthorization, !isRetryAttempt);
                            }
                        }

                        result.Success = response.StatusCode == HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = null;
                result.Exception = ex;
            }

            return result;
        }

        private async Task<bool> RefreshTokens()
        {
            var refresh = _settingsService.RefreshToken;

            if (string.IsNullOrWhiteSpace(refresh))
                throw new InvalidOperationException("Cannot refresh authentication.  Refresh token is null");

            var apiResult = await PostAsync<TokenDto, IEnumerable<TokenDto>>("Auth/refresh", new TokenDto { Token = refresh });

            var result = apiResult.Success;
            var tokens = apiResult?.Data?.Result;
            _settingsService.SetLoginValues(_settingsService.Username, tokens);

            if (!result)
            {
                // post logout event
            }

            return result;
        }



        #endregion
    }
}