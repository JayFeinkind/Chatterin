import UIKit

class viewControllerBase: UIViewController {
    
    var viewModel:viewModelBase? = nil
    
    override func loadView() {
        super.loadView()
        
        if viewModel != nil {
            viewModel?.navigationRequested = navigationRequestedHandler
        }
        
        Task {
            await loadViewModelData()
        }
    }
    
    func loadViewModelData() async {
        if viewModel != nil {
            await viewModel!.start()
            
            dataIsReadyHandler()
        }
    }
    
    func navigationRequestedHandler(viewModel:viewModelBase){
        navigate(viewModel:viewModel)
    }
    
    @MainActor func navigate(viewModel:viewModelBase){
        
    }
    
    @MainActor func dataIsReadyHandler(){
        
    }
}
