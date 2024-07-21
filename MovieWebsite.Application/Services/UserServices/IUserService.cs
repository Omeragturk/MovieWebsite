
using Microsoft.AspNetCore.Identity;
using MovieWebsite.Application.Models.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<IdentityResult> AdminRegister(RegisterDto model);
        Task<IdentityResult> MemberRegister(RegisterDto model);

        Task<SignInResult> Login(LoginDto model);

        Task LogOut();

        Task UpdateUser(UpdateProfileDto model);

        Task<UpdateProfileDto> GetByUserName(string userName);
        Task<bool> UserInRole(string userName, string role);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<bool> UpdateUserStatus(string userName, string status);
    }
}
