using System;
using System.Threading.Tasks;
using Chatterin.Services;

namespace Chatterin.ViewModels
{
    public abstract class ViewModelBase : IViewModelBase
    {
        public Action<IViewModelBase> NavigationRequested { get; set; }
        public Action DataIsReady { get; set; }
        public Action<string, Action> ShowUIMessage { get; set; }
        protected readonly IDependencyService DependencyService;

        public ViewModelBase(IDependencyService dependencyService)
        {
            DependencyService = dependencyService;
        }

        public async Task StartLoadingData()
        {
            await LoadData();
            DataIsReady?.Invoke();
        }

        protected abstract Task LoadData();
    }
}
