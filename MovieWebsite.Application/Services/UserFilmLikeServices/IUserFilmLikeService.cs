using MovieWebsite.Application.Models.DTOs.UserFilmLikeDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Models.VMs.UserFilmLikeViewModels;
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

        Task<UserFilmLikeDto> DislikeFilmAsync(string userId, int filmId);

        Task<IEnumerable<UserFilmLikeVM>> GetDissLikedFilmsByUserAsync(string userId);
        Task<bool> HasUserLikedFilmAsync(string userId, int filmId);
        Task<IEnumerable<UserFilmLikeVM>> GetLikedFilmsByUserAsync(string userId);
        
        Task<UserFilmLikeVM> RemoveLikeAsync(string userId, int filmId);
        
        Task<UserFilmLikeVM> RemoveDislikeAsync(string userId, int filmId);


    }
}
