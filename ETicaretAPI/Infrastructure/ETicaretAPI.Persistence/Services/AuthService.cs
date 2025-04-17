using Azure.Core;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using ETicaretAPI.Application.Features.Commands.User.LoginUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { "540576024167-rdm04qvgfeqn1nfr7to6o5cp0267cc5m.apps.googleusercontent.com" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info);
            }

            Token token = _tokenHandler.CreateAccessToken(100, user.UserName);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1500);

            return token;
        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser? user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
                if (user == null)
                {
                    throw new Exception("Invalid username or email");
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user.UserName);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1500);
                return token;
            }

            throw new Exception("Invalid password");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(1500, user.UserName);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
            {
                throw new Exception("Invalid refresh token");
            }
        }

        public async Task LogoutAsync(string username)
        {
            AppUser? user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                // Clear the refresh token
                user.RefreshToken = null;
                user.RefreshTokenEndDate = null;
                await _userManager.UpdateAsync(user);

                // Sign out the user from the authentication system
                await _signInManager.SignOutAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task GoogleLogoutAsync(string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Find Google login info for this user
                var googleLogins = await _userManager.GetLoginsAsync(user);
                var googleLogin = googleLogins.FirstOrDefault(l => l.LoginProvider == "GOOGLE");

                if (googleLogin != null)
                {
                    // Remove the Google login association
                    await _userManager.RemoveLoginAsync(user, googleLogin.LoginProvider, googleLogin.ProviderKey);
                }

                // Clear the refresh token
                user.RefreshToken = null;
                user.RefreshTokenEndDate = null;
                await _userManager.UpdateAsync(user);

                // Sign out the user from the authentication system
                await _signInManager.SignOutAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

    }
}
