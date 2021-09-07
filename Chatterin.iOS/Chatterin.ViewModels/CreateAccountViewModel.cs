using System;
using System.Text;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using Chatterin.Services;

namespace Chatterin.ViewModels
{
    public class CreateAccountViewModel : ViewModelBase
    {
        readonly ApiService _apiService;

        public CreateAccountViewModel(ApiService apiService, IDependencyService dependencyService) : base(dependencyService)
        {
            _apiService = apiService;
        }

        protected override Task LoadData()
        {
            return Task.FromResult(0);
        }

        private void ShowError(string message, Action callback = null)
        {
            ShowUIMessage?.Invoke(message, callback);
        }

        public async Task CreateAccount(string username, string emailAddress, string password, Action accountCreatedHandler)
        {
            if (string.IsNullOrWhiteSpace(username)) ShowError("Username is required");
            else if (username.Length < 5) ShowError("Username must be atleast 5 characters");
            else if (string.IsNullOrWhiteSpace(emailAddress)) ShowError("Email Address is required");
            else if (string.IsNullOrWhiteSpace(password)) ShowError("Password is required");
            else if (password.Length < 5) ShowError("Password must be atleast 5 characters");
            else
            {
                var newUser = new CreateAccountDto
                {
                    UserName = username,
                    EmailAddress = emailAddress,
                    Password = password
                };

                var apiResult = await _apiService.PostAsync<CreateAccountDto, CreateAccountDto>("registration/create-account", newUser, false);

                if (apiResult.Data.Errors?.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var error in apiResult.Data.Errors)
                    {
                        sb.AppendLine("\u2022 " + error);
                    }
                    ShowError(sb.ToString());
                }
                else
                {
                    ShowError("Account successfully created", accountCreatedHandler);
                }
            }
        }

        public async Task<bool> UserNameAvailable(string userName)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(userName))
            {
                ShowUIMessage?.Invoke("Please enter a username", null);
            }
            else if (userName.Length < 5)
            {
                ShowUIMessage?.Invoke("Username must be atleast 5 characters", null);
            }
            else
            {
                var apiResult = await _apiService.GetAsync<bool>(string.Format("registration/user-name-available?userName={0}", userName), false);

                result = apiResult.Data.Result;
            }

            return result;
        }
    }
}
