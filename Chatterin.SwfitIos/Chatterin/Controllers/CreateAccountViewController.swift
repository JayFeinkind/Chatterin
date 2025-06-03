import UIKit

class CreateAccountViewController: UIViewController, UITextFieldDelegate {
    
   let createAccountModel = CreateAccountModel()
    
    @IBOutlet weak var emailAddressTextField: UITextField!
    @IBOutlet weak var userNameTextField: UITextField!
    @IBOutlet weak var viewContainer: UIView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        
        viewContainer.backgroundColor = UIColor.white.withAlphaComponent(0.25)
        userNameTextField.delegate = self
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        let userName = textField.text
        
        if userName != nil{
            Task{
                await checkIfUserNameAvailable(userName: userName)
            }
        }
    }
    
    func checkIfUserNameAvailable(userName: String?) async{
        let response = await createAccountModel.isUserNameTaken(userName: userName)
        
        if !response.success {
            showError(message: response.errorMessage)
        }
    }
    
    @MainActor func showError(message: String?){
        let alert = UIAlertController(title: "Oops", message: message, preferredStyle: UIAlertController.Style.alert)
        
        alert.addAction(UIAlertAction(title: "Ok", style: .default, handler: nil))
        
        self.present(alert, animated: true)
    }


}



