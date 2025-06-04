import UIKit

class LoginViewController: UIViewController {
    
    @IBOutlet weak var PasswordField: UITextField!
    @IBOutlet weak var UserNameField: UITextField!
    @IBOutlet weak var LoginButton: UIButton!
    @IBOutlet weak var CreateAccountButton: UIButton!
    @IBOutlet weak var ViewContainer: UIView!
    
    let loginModel = LoginModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        
        ViewContainer.backgroundColor = UIColor.white.withAlphaComponent(0.25)
    }

    @IBAction func CreateAccountTouchUp(_ sender: Any) {
        
        let storyboard = UIStoryboard.init(name: "CreateAccount", bundle: nil)
        let controller = storyboard.instantiateInitialViewController()!
        
        navigationController?.pushViewController(controller, animated: true)
    }
    
}

