using System;
using Plugin.Connectivity;

namespace Chatterin.Services
{
    public class IosConnectivityService : ConnectivityService
    {
        public IosConnectivityService()
        {
        }

        public override bool IsConnected => CrossConnectivity.Current.IsConnected;
    }
}
