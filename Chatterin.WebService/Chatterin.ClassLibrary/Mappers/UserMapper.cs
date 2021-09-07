using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chatterin.ClassLibrary.Mappers
{
    public static class UserMapper
    {
        public static IQueryable<UserDto> ToDtos(this IQueryable<User> entities)
        {
            return entities.Select(e => e.ToDto());
        }

        public static UserDto ToDto(this User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                EmailAddress = entity.EmailAddress
            };
        }
    }
}
