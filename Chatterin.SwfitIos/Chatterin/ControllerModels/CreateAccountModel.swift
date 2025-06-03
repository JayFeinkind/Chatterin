//
//  CreateAccountModel.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/2/25.
//

import Foundation

class CreateAccountModel{
    
    let apiService = ApiService<userNameResponseModel>()
    
    func isUserNameTaken(userName: String?) async -> userNameResponseModel {
        var result = userNameResponseModel()
        
        if userName != nil {
            
        }
        
        return result
    }
    
    func getApiResponse() async{
        
        do
        {
            let response = try await apiService.fetchFromAPI(urlStr: "someUrl")
        }
        catch{
            
        }
    }
    
}

struct userNameResponseModel : Decodable{
    var success = false
    var errorMessage = ""
}
