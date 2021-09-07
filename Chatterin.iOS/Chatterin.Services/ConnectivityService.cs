using System;
namespace Chatterin.Services
{
    public abstract class ConnectivityService : IConnectivityService
    {
        public abstract bool IsConnected { get; }
    }
}
