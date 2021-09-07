using System;
using System.Collections.Generic;
using Chatterin.ClassLibrary;

namespace Chatterin.Services
{
    public interface ISettingsService
    {
        string JwtToken { get; set; }
        string RefreshToken { get; set; }
        string Username { get; set; }
        int UserId { get; set; }
        void LoadSettings();
        void SaveSettings();
        void SetLoginValues(string userName, IEnumerable<TokenDto> tokens);
    }
}
