import UIKit

class LoginViewController: viewControllerBase {
    
    var loginViewModel:LoginViewModel {
        return super.viewModel as! LoginViewModel
    }
    
    @IBOutlet weak var PasswordField: UITextField!
    @IBOutlet weak var UserNameField: UITextField!
    @IBOutlet weak var LoginButton: UIButton!
    @IBOutlet weak var CreateAccountButton: UIButton!
    @IBOutlet weak var ViewContainer: UIView!
    
    override func loadView() {
        
        super.viewModel = LoginViewModel()
        
        super.loadView()
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        
        ViewContainer.backgroundColor = UIColor.white.withAlphaComponent(0.25)
    }
    
    override func navigate(viewModel: viewModelBase) {
        let storyboard = UIStoryboard.init(name: "CreateAccount", bundle: nil)
        let controller = storyboard.instantiateInitialViewController()! as viewControllerBase
        controller.viewModel = viewModel
        
        navigationController?.pushViewController(controller, animated: true)
    }

    @IBAction func CreateAccountTouchUp(_ sender: Any) {
        loginViewModel.navigateToCreateAccount()
    }
    
}

