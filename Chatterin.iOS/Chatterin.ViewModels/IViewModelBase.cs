using System;
using System.Threading.Tasks;

namespace Chatterin.ViewModels
{
    public interface IViewModelBase
    {
        Action<IViewModelBase> NavigationRequested { get; set; }
        Action DataIsReady { get; set; }
        Action<string, Action> ShowUIMessage { get; set; }
        Task StartLoadingData();
    }
}
