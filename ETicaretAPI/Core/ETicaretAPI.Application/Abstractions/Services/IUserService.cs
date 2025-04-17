using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate, int refreshTokenLifeTime);
        Task<string> GetCurrentUserId();
        Task<AppUser> GetCurrentUser();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByUserName(string userName);
        Task<List<AppUser>> GetUsersByList(List<string> userIds);
        Task UpdateUserProfile(string name, string phone);
        Task<List<ListUser>> GetAllUsersAsync();
    }
}
