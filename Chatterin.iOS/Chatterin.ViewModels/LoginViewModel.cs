using System;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using Chatterin.Services;

namespace Chatterin.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        CreateSqliteService _createSqliteService;
        IAuthenticationService _authenticationService;

        public LoginViewModel(
            IDependencyService dependencyService,
            CreateSqliteService createSqliteService,
            IAuthenticationService authenticationService) : base(dependencyService)
        {
            _authenticationService = authenticationService;
            _createSqliteService = createSqliteService;
        }

        protected override async Task LoadData()
        {
            await _createSqliteService.SetupDb();
        }

        public void NavigateToCreateAccount()
        {
            var viewModel = DependencyService.Resolve<CreateAccountViewModel>();
            NavigationRequested?.Invoke(viewModel);
        }

        public async Task<bool> Login(string userName, string password)
        {
            var success = false;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                ShowUIMessage?.Invoke("Username and password are required", null);
            }
            else
            {
                var result = await _authenticationService.Login(userName, password);

                if (!result.Success)
                {
                    ShowUIMessage?.Invoke("Username or password does not match our records.", null);
                }
                else
                {
                    success = true;
                    NavigateToConversations();
                }
            }

            return success;
        }

        private void NavigateToConversations()
        {
            var viewModel = DependencyService.Resolve<ConversationCatalogViewModel>();
            NavigationRequested?.Invoke(viewModel);
        }
    }
}
