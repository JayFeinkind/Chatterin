import Foundation

/// Sort of trying to replicate an abstract class with these, not sure if it would just be better to use a normal class instead

protocol viewModelBase {
    func loadData() async
    var navigationRequested:((viewModelBase) -> Void)? { get set }
}

extension viewModelBase {
    
    func start() async {
        await loadData()
    }

    func navigateToViewModel(viewModel:viewModelBase){
        navigationRequested?(viewModel)
    }
}
