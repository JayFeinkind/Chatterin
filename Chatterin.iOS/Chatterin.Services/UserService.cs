using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;

namespace Chatterin.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(ApiService apiService, FileService context, IConnectivityService connectivityService) : base(apiService, context, connectivityService)
        {
        }

        protected override string UrlAppendage => "Accounts/users";
    }
}
