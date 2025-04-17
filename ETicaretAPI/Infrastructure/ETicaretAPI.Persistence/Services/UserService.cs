using Azure.Core;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = model.Id,
                NameSurname = model.NameSurname,
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);

            if (result.Succeeded)
            {
                return new()
                {
                    Success = true,
                    Message = "User created successfully"
                };
            }
            else
            {
                return new()
                {
                    Success = false,
                    Message = "User created failed"
                };
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("User not found");
        }

        public async Task<string> GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                return null;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ??
                         user.FindFirstValue(ClaimTypes.Name);

            if (!string.IsNullOrEmpty(userId))
                return userId;

            var username = user.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                var appUser = await _userManager.FindByNameAsync(username);
                return appUser?.Id;
            }

            return null;
        }

        public async Task<AppUser> GetCurrentUser()
        {
            var userId = await GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return null;

            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<AppUser> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public Task<AppUser> GetUserByUserName(string userName)
        {
            var user = _userManager.FindByNameAsync(userName);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<List<AppUser>> GetUsersByList(List<string> userIds)
        {
            var userList = await _userManager.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();

            return userList;
        }

        public async Task UpdateUserProfile(string name, string phone)
        {
            var userId = await GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not found");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            user.NameSurname = name;
            user.PhoneNumber = phone;

            await _userManager.UpdateAsync(user);
        }

        public async Task<List<ListUser>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            
            return users.Select(user => new ListUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.NameSurname,
                PhoneNumber = user.PhoneNumber
            }).ToList();
        }

    }
}
