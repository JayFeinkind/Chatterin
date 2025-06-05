//
//  ApiService.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/3/25.
//

import Foundation

class ApiService {
    
    func postToAPI<PostType: Encodable, ReturnType: Decodable>(urlStr: String, postData: PostType, addAuth: Bool) async throws -> ApiResult<ReturnType> {

        let url = URL(string: "https://httpbin.org/post")!

        var request = URLRequest(url: url)

        request.httpMethod = "POST"
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")
        
        if addAuth {
            //request.setValue("bearer ?", forHTTPHeaderField: "Authorization")
        }

        let jsonData = try JSONEncoder().encode(postData)
        request.httpBody = jsonData

        let (data, response) = try await URLSession.shared.data(for: request)
        
        /// there should be additional logic around status codes.  No point in deserializing if not a 200 response
        let statusCode = (response as? HTTPURLResponse)?.statusCode
        
        print(statusCode ?? "nil")
        
        //return statusCode != nil && (200...299).contains(statusCode!)
        
        let result = try JSONDecoder().decode(ApiResult<ReturnType>.self, from: data)
        
        return result
    }
    
    func fetchFromAPI<T: Decodable>(urlStr: String) async throws -> ApiResult<T> {
        
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
