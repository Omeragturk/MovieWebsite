using Microsoft.AspNetCore.Identity;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.Repositories
{
    public class UserRepo : BaseRepo<User>, IUserRepository
    {
        UserManager<User> _userManager;
        public UserRepo(AppDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public override Task Create(User entity)
        {
            _userManager.CreateAsync(entity);
            return base.Create(entity);
        }

        
    }
}
