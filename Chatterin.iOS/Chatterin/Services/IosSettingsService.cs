using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chatterin.ClassLibrary;
using Foundation;

namespace Chatterin.Services
{
    public class IosSettingsService : ISettingsService
    {
        private string _username;
        private const string _usernameKey = "username";

        private int _userId;
        private const string _userIdKey = "user_id";

        private string _jwtToken;
        private const string _jwtTokenKey = "jwt_token";

        private string _refreshToken;
        private const string _refreshTokenKey = "refresh_token";

        private string _userSettingsFilePath = string.Empty;
        private string _settingsFolder = string.Empty;


        public IosSettingsService()
        {
            
        }

        public void LoadSettings()
        {
            LoadPrivateSettings();
            LoadPublicSettings();
        }

        public void SaveSettings()
        {
            SavePrivateSettings();
            SavePublicSettings();
        }

        public void SetLoginValues(string userName, IEnumerable<TokenDto> tokens)
        {
            var jwtToken = tokens?.FirstOrDefault(t => t.TokenType == TokenTypes.JWT);
            _userId = jwtToken?.ClientId != null ? int.Parse(jwtToken.ClientId) : 0;
            _username = userName;
            _jwtToken = jwtToken?.Token;
            _refreshToken = tokens?.FirstOrDefault(t => t.TokenType == TokenTypes.Refresh)?.Token;
            SaveSettings();
        }

        private void LoadPrivateSettings()
        {
            try
            {
                //you can't write to files in the bundle; need to read and write a custom
                //plist file to a local directory:
                string fileName = "UserSettings.plist";
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                _settingsFolder = System.IO.Path.Combine(docFolder, "..", "Library", "CustomSettings");
                _userSettingsFilePath = System.IO.Path.Combine(_settingsFolder, fileName);

                NSDictionary settingsDictionary = null;
                if (File.Exists(_userSettingsFilePath))
                {
                    settingsDictionary = new NSDictionary(_userSettingsFilePath);
                    _jwtToken = (NSString)settingsDictionary[_jwtTokenKey];
                    _refreshToken = (NSString)settingsDictionary[_refreshTokenKey];
                    _username = (NSString)settingsDictionary[_usernameKey];
                    _userId = (int)((NSNumber)settingsDictionary[_userIdKey]);
                }
                else
                {
                    settingsDictionary = new NSDictionary(NSBundle.MainBundle.PathForResource("UserSettings.plist", null));
                }
            }
            catch 
            {
            }
        }

        private void LoadPublicSettings()
        {
            try
            {
               // _baseUrl = NSUserDefaults.StandardUserDefaults.StringForKey(_baseUrlKey);
                
            }
            catch 
            {
                
            }
        }

        private void SavePrivateSettings()
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(_settingsFolder))
                {
                    Directory.CreateDirectory(_settingsFolder);
                }
                var settingsDictionary = new NSMutableDictionary(NSBundle.MainBundle.PathForResource("UserSettings.plist", null));

                settingsDictionary.SetValueForKey((NSString)this.JwtToken, (NSString)_jwtTokenKey);
                settingsDictionary.SetValueForKey((NSString)this.RefreshToken, (NSString)_refreshTokenKey);
                settingsDictionary.SetValueForKey((NSString)this.Username, (NSString)_usernameKey);
                settingsDictionary.SetValueForKey((NSNumber)this.UserId, (NSString)_userIdKey);

                result = settingsDictionary.WriteToFile(_userSettingsFilePath, false);
            }
            catch 
            {
            }
        }

        private void SavePublicSettings()
        {
            try
            {
                //NSUserDefaults.StandardUserDefaults.SetString(_baseUrl, _baseUrlKey);
            }
            catch 
            {

            }
        }

        //public bool UploadLogs
        //{
        //    get
        //    {
        //        return NSUserDefaults.StandardUserDefaults.BoolForKey(_uploadLogsKey);
        //    }
        //    set
        //    {
        //        NSUserDefaults.StandardUserDefaults.SetBool(value, _uploadLogsKey);
        //        NSUserDefaults.StandardUserDefaults.Synchronize();
        //    }
        //}

        //public bool UploadPList
        //{
        //    get
        //    {
        //        return NSUserDefaults.StandardUserDefaults.BoolForKey(_uploadPList);
        //    }
        //    set
        //    {
        //        NSUserDefaults.StandardUserDefaults.SetBool(value, _uploadPList);
        //        NSUserDefaults.StandardUserDefaults.Synchronize();
        //    }
        //}

        public string JwtToken
        {
            get
            {
                return _jwtToken;
            }
            set
            {
                _jwtToken = value;
            }
        }

        public string RefreshToken
        {
            get
            {
                return _refreshToken;
            }
            set
            {
                _refreshToken = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }
    }
}


