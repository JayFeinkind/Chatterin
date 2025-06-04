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
            let apiResponse: ApiResult<Bool> = await getApiResponse(url:apiHelper.isUserNameTakenUrl)
            
            result.success = apiResponse.success
            result.isUserNameTaken = apiResponse.result ?? false
            
            if result.success != true && apiResponse.errorMessage.count > 0 {
                result.errorMessage = apiResponse.errorMessage[0]
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
        catch {
            result.success = false
            result.errorMessage.append("Unable to contact server")
        }
        
        return result
    }
}
