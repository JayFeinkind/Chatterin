//
//  CreateAccountModel.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/2/25.
//

import Foundation

class CreateAccountModel {

    func isUserNameTaken(userName: String?) async -> userNameResponseModel {
        var result = userNameResponseModel()
        
        if userName != nil {
            let url = apiHelper.isUserNameTakenUrl + "?userName=" + userName!
            
            let apiResponse: ApiResult<Bool> = await getApiResponse(url:url)
            
            result.success = apiResponse.success
            result.isUserNameAvailable = apiResponse.result ?? false
            
            if result.success != true && apiResponse.errors.count > 0 {
                result.errorMessage = apiResponse.errors[0]
            }
        }
        
        return result
    }
    
    func getApiResponse<T: Decodable>(url: String) async -> ApiResult<T>{
        var result = ApiResult<T>()
        let apiService = ApiService<T>()
        
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
