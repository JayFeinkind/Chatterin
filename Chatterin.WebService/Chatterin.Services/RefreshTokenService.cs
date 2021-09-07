using Chatterin.ClassLibrary;
using Chatterin.ClassLibrary.Mappers;
using Chatterin.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public class RefreshTokenService : ServiceBase<RefreshToken>, IRefreshTokenService
    {
        public RefreshTokenService(ChatterinContext context) : base(context)
        { }

        public async Task<RefreshTokenDto> CreateRefreshToken(RefreshTokenDto dto)
        {
            Context.RefreshTokens.Add(dto.ToEntity());
            await Context.SaveChangesAsync();
            return dto;
        }

        public async Task<RefreshTokenDto> UpdateRefreshToken(RefreshTokenDto dto)
        {
            Context.RefreshTokens.Update(dto.ToEntity());
            await Context.SaveChangesAsync();
            return dto;
        }

        public async Task DeleteTokens(int userId)
        {
            var entities = await PersistedQuery.Where(t => t.UserId == userId).ToListAsync();
            Context.RemoveRange(entities);
            await Context.SaveChangesAsync();
        }

        public async Task<RefreshTokenDto> SelectRefreshToken(int userId, string token)
        {
            var currentTime = DateTime.UtcNow;

            return await ReadOnlyQuery
                .Where(rt => rt.Token == token && rt.UserId == userId && rt.TokenExpiredUtc > currentTime)
                .ToDtos()
                .FirstOrDefaultAsync();
        }
    }
}
