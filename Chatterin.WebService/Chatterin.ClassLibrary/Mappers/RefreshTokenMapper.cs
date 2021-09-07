using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chatterin.ClassLibrary.Mappers
{
    public static class RefreshTokenMapper
    {
        public static IQueryable<RefreshTokenDto> ToDtos(this IQueryable<RefreshToken> entities)
        {
            return entities.Select(e => e.ToDto());
        }

        public static RefreshTokenDto ToDto(this RefreshToken entity)
        {
            return new RefreshTokenDto
            {
                Token = entity.Token,
                TokenExpiredUtc = entity.TokenExpiredUtc,
                UserId = entity.UserId
            };
        }

        public static IQueryable<RefreshToken> ToEntities(this IQueryable<RefreshTokenDto> dtos)
        {
            return dtos.Select(d => d.ToEntity());
        }

        public static RefreshToken ToEntity(this RefreshTokenDto dto)
        {
            return new RefreshToken
            {
                Token = dto.Token,
                TokenExpiredUtc = dto.TokenExpiredUtc,
                UserId = dto.UserId
            };
        }
    }
}
