using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public Application.DTOs.Token CreateAccessToken(int accessTokenLifeTime, string username)
        {
            Application.DTOs.Token token = new Application.DTOs.Token();

            var userRoles = _userManager.GetRolesAsync(_userManager.FindByNameAsync(username).Result).Result;

            var userId = _userManager.FindByNameAsync(username).Result.Id;

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string securityKey = _configuration["TokenOptions:SecurityKey"];
            if (string.IsNullOrEmpty(securityKey))
            {
                throw new ArgumentNullException("TokenOptions:SecurityKey", "Security key cannot be null or empty.");
            }

            SymmetricSecurityKey symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            SigningCredentials signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            DateTime expiration = DateTime.Now.AddSeconds(accessTokenLifeTime);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["TokenOptions:Issuer"],
                audience: _configuration["TokenOptions:Audience"],
                notBefore: DateTime.Now,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.Expiration = expiration;

            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }

}
