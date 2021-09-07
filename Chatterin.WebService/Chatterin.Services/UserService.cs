using Chatterin.ClassLibrary;
using Chatterin.ClassLibrary.Mappers;
using Chatterin.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(ChatterinContext context) : base(context) { }

        public async Task<ApiResult<IEnumerable<UserDto>>> GetUsers()
        {
            return new ApiResult<IEnumerable<UserDto>> { Result = await ReadOnlyQuery.ToDtos().ToListAsync() };
        }
       
        public async Task<ApiResult<bool>> UserNameAvailable(string userName)
        {
            return new ApiResult<bool>
            {
                Result = !await UsernameIsRegistered(userName)
            };
        }

        private async Task<bool> EmailAddressIsRegistered(string emailAddress)
        {
            return await ReadOnlyQuery.AnyAsync(u => u.EmailAddress.ToLower() == emailAddress.ToLower());
        }

        private async Task<bool> UsernameIsRegistered(string userName)
        {
            return await ReadOnlyQuery.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
        }

        private async Task<CreateAccountDto> CreateUser(string userName, string password, string emailAddress)
        {
            var salt = new byte[8];
            new Random().NextBytes(salt);

            var passwordBytes = Encoding.ASCII.GetBytes(password);
            using var hasher = SHA256.Create();
            var passwordHashed = hasher.ComputeHash(passwordBytes.Concat(salt).ToArray());

            var user = new User
            {
                EmailAddress = emailAddress,
                UserName = userName,
                Membership = new Membership
                {
                    Salt = salt,
                    Password = passwordHashed
                }
            };

            Context.Users.Add(user);

            await Context.SaveChangesAsync();

            return new CreateAccountDto
            {
                Id = user.Id,
                EmailAddress = user.EmailAddress,
                UserName = user.UserName
            };
        }

        public async Task<ApiResult<CreateAccountDto>> CreateUser(CreateAccountDto dto)
        {
            var result = new ApiResult<CreateAccountDto>();

            try
            {
                if (string.IsNullOrWhiteSpace(dto.UserName)) result.Errors.Add("Username is required");
                else if (await UsernameIsRegistered(dto.UserName)) result.Errors.Add("Username is alraedy taken");
                else if (dto.UserName.Length < 5) result.Errors.Add("Username must be atleast 5 characters");

                if (string.IsNullOrWhiteSpace(dto.EmailAddress)) result.Errors.Add("Email Address is required");
                else if (await EmailAddressIsRegistered(dto.EmailAddress)) result.Errors.Add("Email Address is already registered");
                else if (!dto.EmailAddress.IsValidEmail()) result.Errors.Add("Email format is invalid");

                if (string.IsNullOrWhiteSpace(dto.Password)) result.Errors.Add("Password is required");
                else if (dto.Password.Length < 5) result.Errors.Add("Password must be atleast 5 characters");

                if (result.Errors.Count == 0)
                {
                    result.Result = await CreateUser(dto.UserName, dto.Password, dto.EmailAddress);
                }
            }
            catch (Exception e)
            {
                
            }

            return result;
        }
    }
}
