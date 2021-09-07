using System;
using Unity;

namespace Chatterin.Services
{
    public class DependencyService : IDependencyService
    {
        private static IUnityContainer _dependencyContainer;

        public DependencyService()
        {
            _dependencyContainer = new UnityContainer();
            RegisterDependencies();
        }

        public T Resolve<T>()
        {
            return _dependencyContainer.Resolve<T>();
        }

        public void RegisterDependencies()
        {
            //var eventAggregator = new EventAggregator();
            //_dependencyContainer.RegisterInstance<IEventAggregator>(eventAggregator);
            //_dependencyContainer.RegisterType<ICreateSqliteService, CreateSqliteService>();
            //_dependencyContainer.RegisterType<ISqliteConnectionService, SqliteConnectionService>();
            _dependencyContainer.RegisterType<IAuthenticationService, AuthenticationService>();
            _dependencyContainer.RegisterType<IUserService, UserService>();
            _dependencyContainer.RegisterType<IConversationService, ConversationService>();
        }

        public void RegisterInstance<T>(T implementation)
        {
            _dependencyContainer.RegisterInstance<T>(implementation);
        }

        public void RegisterType<T, T1>() where T1 : T
        {
            _dependencyContainer.RegisterType<T, T1>();
        }
    }
}