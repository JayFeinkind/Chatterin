import UIKit

class CreateAccountViewController: UIViewController, UITextFieldDelegate {
    
   let createAccountViewModel = CreateAccountViewModel()
    
    @IBOutlet weak var confirmPasswordTextField: UITextField!
    @IBOutlet weak var PasswordTextField: UITextField!
    @IBOutlet weak var emailAddressTextField: UITextField!
    @IBOutlet weak var userNameTextField: UITextField!
    @IBOutlet weak var viewContainer: UIView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        viewContainer.backgroundColor = UIColor.white.withAlphaComponent(0.25)
        userNameTextField.delegate = self
    }
    
    @IBAction func createAccountPressed(_ sender: Any) {
        
        let userName = userNameTextField.text
        let email = emailAddressTextField.text
        let password = PasswordTextField.text
        let confirm = confirmPasswordTextField.text

        if let error = createAccountViewModel.validateAccountInfo(userName: userName, emailAddress: email, password: password, confirmPassword: confirm) {
            showError(message: error)
        } else {
            Task {
                await createAccount(userName: userName!, email: email!, password: password!)
            }
        }
    }
    
    func createAccount(userName: String, email: String, password: String) async {
        let createResult = await createAccountViewModel.createAccount(userName: userName, emailAddress: email, password: password)
        
        if createResult.success {
            //Navigate
        } else if createResult.errors.count > 0 {
            showError(message: createResult.errors[0])
        } else {
            showError(message: "Unable to create account")
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        let userName = textField.text
        
        if userName != nil {
            Task{
                await checkIfUserNameAvailable(userName: userName)
            }
        }
    }
    
    func checkIfUserNameAvailable(userName: String?) async {
        await createAccountViewModel.checkUserNameTaken(userName: userName)
    }
    
    @MainActor func showError(message: String?) {
        let alert = UIAlertController(title: "Oops", message: message, preferredStyle: UIAlertController.Style.alert)
        
        alert.addAction(UIAlertAction(title: "Ok", style: .default, handler: nil))
        
        self.present(alert, animated: true)
    }
}
