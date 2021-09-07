using System;
namespace Chatterin.Services
{
    public interface IDependencyService
    {
        T Resolve<T>();
        void RegisterInstance<T>(T implementation);
        void RegisterType<T, T1>() where T1 : T;
    }
}
