//
//  CreateAccountModel.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/2/25.
//

import Foundation

class createAccountViewModel: viewModelBase {
    
    func loadData() async {
        
    }
    
    var navigationRequested: ((viewModelBase) -> Void)?
    
    
    var isUserNameAvailable = false
    let apiService = ApiService()
    
    func validateAccountInfo(userName: String?, emailAddress: String?, password: String?, confirmPassword: String?) -> String? {
        
        var result: String? = nil
        
        if userName == nil {
            result = "Username is required"
        } else if userName!.count <= 3 {
            result = "Username must be at least 4 characters"
        } else if userName!.count >= 15 {
            result = "Username must be less then 16 characters"
        } else if emailAddress == nil {
            result = "Email is required"
        } else if isValidEmail(emailAddress!) == false {
            result = "Email format is invalid"
        } else if password == nil {
            result = "Password is required"
        } else if password!.count <= 3 {
            result = "Password must be at least 3 characters"
        } else if password!.count > 50 {
            result = "Password must be less then 50 characters"
        } else if password != confirmPassword {
            result = "Passwords do not match"
        } else if isUserNameAvailable == false {
            result = "Username is already taken"
        }
        
        return result
    }
    
    func isValidEmail(_ email: String) -> Bool {
        let emailRegEx = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}"

        let emailPred = NSPredicate(format:"SELF MATCHES %@", emailRegEx)
        return emailPred.evaluate(with: email)
    }

    func checkUserNameTaken(userName: String?) async {
        if userName != nil {
            let apiResponse: ApiResult<Bool> = await getIsUsernameTaken(userName:userName!)
            
            isUserNameAvailable = apiResponse.success && (apiResponse.result ?? false)
        }
    }
    
    func createAccount(userName: String, emailAddress: String, password: String) async -> ApiResult<createAccountModel> {
        var result = ApiResult<createAccountModel>()
        let url = apiHelper.createAccountUrl
        var postData = createAccountModel()
        
        postData.userName = userName
        postData.password = password
        postData.emailAddress = emailAddress
        
        do {
            result = try await apiService.postToAPI(urlStr: url, postData: postData, addAuth: false)
        }
        catch let error{
            result.success = false
            result.errors.append("Unable to contact server")
            print(error.localizedDescription)
        }

        return result
    }
    
    func getIsUsernameTaken(userName: String) async -> ApiResult<Bool> {
        var result = ApiResult<Bool>()
        let url = apiHelper.isUserNameTakenUrl + "?userName=" + userName
        
        do {
            result = try await apiService.fetchFromAPI(urlStr: url)
        }
        catch let error{
            result.success = false
            result.errors.append("Unable to contact server")
            print(error.localizedDescription)
        }
        
        return result
    }
}
