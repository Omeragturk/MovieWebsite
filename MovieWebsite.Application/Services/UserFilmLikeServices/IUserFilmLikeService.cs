using MovieWebsite.Application.Models.DTOs.UserFilmLikeDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.UserFilmLikeServices
{
    public interface IUserFilmLikeService
    {
        Task<UserFilmLikeDto> LikeFilmAsync(string userId, int filmId);
        Task DislikeFilmAsync(string userId, int filmId);
        Task<bool> HasUserLikedFilmAsync(string userId, int filmId);
        Task<List<FilmVM>> GetLikedFilmsByUserAsync(string userId);
    }
}
