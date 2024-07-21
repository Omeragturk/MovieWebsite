using Microsoft.EntityFrameworkCore;
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
    public class UserFilmLikeRepo : BaseRepo<UserFilmLike>, IUserFilmLikeRepo
    {
        private readonly AppDbContext _context;
        public UserFilmLikeRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Film>> GetLikedFilmsByUserId(string userId)
        {
            return await _context.Set<UserFilmLike>()
                .Where(ufl => ufl.UserId == userId)
                .Select(ufl => ufl.Film)
                .ToListAsync();
        }

        public Task<List<User>> GetUsersWhoLikedFilm(int filmId)
        {
            return _context.Set<UserFilmLike>()
                .Where(ufl => ufl.FilmId == filmId)
                .Select(ufl => ufl.User)
                .ToListAsync();
        }
    }

        
    
}
