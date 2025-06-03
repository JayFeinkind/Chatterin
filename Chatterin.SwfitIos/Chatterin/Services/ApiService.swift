//
//  ApiService.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/3/25.
//

import Foundation

class ApiService<T: Decodable>{
    
    func fetchFromAPI(urlStr: String) async throws -> ApiResult<T> {
        
        var result = ApiResult<T>()
        
        let url = URL(string: urlStr)!

        let (data, _) = try await URLSession.shared.data(from: url)

        result.data = try JSONDecoder().decode(T.self, from: data)

        return result
    }
    
}

struct ApiResult<T>{
    var success = false
    var data: T? = nil
    var errorMessage = ""
}
