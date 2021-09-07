using System;
namespace Chatterin.Services
{
    public interface IConnectivityService
    {
        bool IsConnected { get; }
    }
}
