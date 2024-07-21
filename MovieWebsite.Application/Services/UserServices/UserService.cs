using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Models.DTOs.UserDtos;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace MovieWebsite.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }



        
        public async Task<IdentityResult> AdminRegister(RegisterDto model)
        {
            var user = _mapper.Map<User>(model);
            user.ImagePath = $"/images/01-admin.jpg";

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "ADMIN");

                await _signInManager.SignInAsync(user, false);
            }


            return result;
        }

        public async Task<UpdateProfileDto> GetByUserName(string userName)
        {
            var user = await _userRepository.GetFilteredFirstOrDefault(
                            select: x => _mapper.Map<UpdateProfileDto>(x),
                            where: x => x.UserName.Equals(userName) && x.Status != Status.Passive);

            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetFilteredList(
                               select: x => _mapper.Map<UserDto>(x),
                                              where: x => x.Status != Status.Passive);
            return users;
          
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            var user = await _userRepository.GetDefault(x => x.UserName.Equals(model.UserName));
            if (user == null || user.Status == Status.Passive)
            {
                return SignInResult.Failed;

            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                return result;
            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> MemberRegister(RegisterDto model)
        {
            var user = _mapper.Map<User>(model);
            user.ImagePath = $"/images/02-member.jpg";

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "MEMBER");

                await _signInManager.SignInAsync(user, false);
            }

            return result;

        }

        public async Task UpdateUser(UpdateProfileDto model)
        {

            var user = await _userRepository.GetDefault(x => x.Id.Equals(model.Id));

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/UsersImages/{guid}.jpg");
                user.ImagePath = $"/images/UsersImages/{guid}.jpg";

                await _userRepository.Update(user);
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;


            if (!string.IsNullOrEmpty(model.Password))
            {
                var isUserNameExist = await _userManager.FindByNameAsync(model.UserName);

                if (isUserNameExist == null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                    await _signInManager.SignInAsync(user, false);
                }
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                var isUserEmailExist = await _userManager.FindByEmailAsync(model.Email.ToUpper());

                if (isUserEmailExist == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }
        }

        public async Task<bool> UpdateUserStatus(string userName, string status)
        {
            var user = await _userRepository.GetDefault(x => x.UserName.Equals(userName));

            if (user == null)
            {
                return false;
            }

            if (status == "Active")
            {
                user.Status = Status.Active;
            }
            else
            {
                user.Status = Status.Passive;
            }

            await _userRepository.Update(user);
            return true;
        }

        public async Task<bool> UserInRole(string userName, string role)
        {

            var user = await _userManager.FindByNameAsync(userName);
            bool isInRole = await _userManager.IsInRoleAsync(user, role);

            return isInRole;
        }

        
    }
}
