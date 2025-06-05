//
//  RegisterAccountModel.swift
//  Chatterin
//
//  Created by Jason Feinkind on 6/5/25.
//

import Foundation

struct createAccountModel: Encodable, Decodable{
    var userName: String = ""
    var emailAddress: String = ""
    var password: String = ""
}
