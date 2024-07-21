using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Domain.Interfaces
{
    public interface IUserFilmLikeRepo : IBaseRepository<UserFilmLike>
    {
        Task<List<Film>> GetLikedFilmsByUserId(string userId);
        Task<List<User>> GetUsersWhoLikedFilm(int filmId);
    }
}
