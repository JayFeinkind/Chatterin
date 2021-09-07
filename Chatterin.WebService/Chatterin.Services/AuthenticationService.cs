using Chatterin.ClassLibrary;
using Chatterin.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public class AuthenticationService : ServiceBase<Membership>, IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        IRefreshTokenService _refreshTokenService;
        int _jwtTokenExpiresSeconds;
        int _refreshTokenExpiresSeconds;


        public AuthenticationService(ChatterinContext context, IOptions<AppSettings> appSettings, IRefreshTokenService refreshTokenService) : base(context)
        {
            _refreshTokenService = refreshTokenService;
            _appSettings = appSettings.Value;

            _jwtTokenExpiresSeconds = int.Parse(_appSettings.JWTTimeoutHours) * 60 * 60;
            _refreshTokenExpiresSeconds = int.Parse(_appSettings.RefreshTimeoutHours) * 60 * 60;
        }

        private async Task<Membership> GetMembershipByUsername(string userName)
        {
            var user = await Context.Users.Include(u => u.Membership).FirstOrDefaultAsync(u => u.UserName == userName);
            return user?.Membership;
        }

        public async Task<ApiResult<IEnumerable<TokenDto>>> Authenticate(string username, string password)
        {
            var result = new ApiResult<IEnumerable<TokenDto>>();
            var membership = await GetMembershipByUsername(username);

            //check user's hash(password+salt) against the stored database value
            using var hasher = SHA256.Create();
            var salt = membership?.Salt ?? new byte[0];
            var hashedPassword = hasher.ComputeHash(password.Select(c => (byte)c).Concat(salt).ToArray());

            if (membership == null || !hashedPassword.SequenceEqual(membership.Password))
            {
                result.Errors.Add(Codes.BadCredentials);
            }
            else
            {
                var jwt = GenerateAccessToken(membership.Id);
                
                var refresh = await GenerateRefreshToken(membership.Id);

                result.Result = new List<TokenDto> { jwt, refresh };
            }

            return result;
        }

        private TokenDto GenerateAccessToken(int userId)
        {
            var claims = new List<Claim>() { new Claim(Codes.UserId, userId.ToString()) };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwtTokenExpiresSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var jwt = new TokenDto
            {
                ClientId = userId.ToString(),
                TokenType = "JWT",
                Token = tokenString,
                TokenParams = new Dictionary<string, string>
                {
                    { "expiresIn", _jwtTokenExpiresSeconds.ToString() },
                    { "issueTime", DateTime.UtcNow.Ticks.ToString() }
                }
            };

            return jwt;
        }

        private async Task<TokenDto> GenerateRefreshToken(int userId)
        {
            //generate and store a refresh token for the user
            using var RNG = RandomNumberGenerator.Create();
            var refreshBytes = new byte[32];
            RNG.GetBytes(refreshBytes);
            var refreshToken = Convert.ToBase64String(refreshBytes);

            var refresh = GenerateRefreshToken(refreshToken);

            var hashedRefreshToken = HashToken(refresh.Token);

            await _refreshTokenService.CreateRefreshToken(new RefreshTokenDto
            {
                Token = hashedRefreshToken,
                UserId = userId,
                TokenExpiredUtc = DateTime.UtcNow.AddSeconds(_refreshTokenExpiresSeconds)
            });

            return refresh;
        }

        private TokenDto GenerateRefreshToken(string existingToken)
        {
            var refresh = new TokenDto
            {
                TokenType = "Refresh",
                Token = existingToken,
                TokenParams = new Dictionary<string, string>
                {
                    { "expiresIn", _refreshTokenExpiresSeconds.ToString() },
                    { "issueTime", DateTime.UtcNow.Ticks.ToString() }
                }
            };
            return refresh;
        }

        private string HashToken(string refreshToken)
        {
            using var hasher = SHA256.Create();
            return Convert.ToBase64String(hasher.ComputeHash(Convert.FromBase64String(refreshToken)));
        }

        public async Task<ApiResult<IEnumerable<TokenDto>>> RefreshTokens(string bearerToken, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //get the user name and impersonating user name from the bearer token
            var token = tokenHandler.ReadJwtToken(bearerToken);
            var userId = Int32.Parse(token.Claims.First(c => c.Type == Codes.UserId).Value);

            //Validate and renew the refresh token. If successful, also create and return the JWT.
            return new ApiResult<IEnumerable<TokenDto>> { Result = await Refresh(refreshToken, userId) };
        }

        private async Task<IEnumerable<TokenDto>> Refresh(string refreshToken, int userId)
        {
            var refresh = await RenewRefreshToken(refreshToken, userId);
            if (refresh == null)
            {
                return null;
            }

            var jwt = GenerateAccessToken(userId);

            return new[] { jwt, refresh };
        }

        private async Task<TokenDto> RenewRefreshToken(string refreshToken, int userId)
        {
            var hashedRefreshToken = HashToken(refreshToken);

            var existingToken = await _refreshTokenService.SelectRefreshToken(userId, hashedRefreshToken);
            if (existingToken == null)
            {
                return null;
            }

            var updatedToken = GenerateRefreshToken(refreshToken);
            existingToken.TokenExpiredUtc = DateTime.UtcNow.AddSeconds(_refreshTokenExpiresSeconds);

            await _refreshTokenService.UpdateRefreshToken(existingToken);

            return updatedToken;
        }
    }
}
