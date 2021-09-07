using Chatterin.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenDto> CreateRefreshToken(RefreshTokenDto dto);
        Task<RefreshTokenDto> UpdateRefreshToken(RefreshTokenDto dto);
        Task<RefreshTokenDto> SelectRefreshToken(int userId, string token);
        Task DeleteTokens(int userId);
    }
}
