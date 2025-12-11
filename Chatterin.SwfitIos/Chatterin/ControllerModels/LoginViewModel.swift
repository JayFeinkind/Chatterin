//
//  LoginModel.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/2/25.
//

import Foundation

class LoginViewModel: viewModelBase {
    var navigationRequested: ((viewModelBase) -> Void)? = nil
    

    

    
   
    func navigateToCreateAccount(){
        navigationRequested?(createAccountViewModel())
    }
    
    
    func loadData() async {
        
    }
    
    
    let apiService = ApiService()
}
