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

        result = try JSONDecoder().decode(ApiResult<T>.self, from: data)

        return result
    }
    
}

struct ApiResult<T: Decodable>: Decodable{
    var success = false
    var result: T? = nil
    var errors: [String] = []
}
