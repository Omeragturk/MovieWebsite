using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Application.Models.DTOs.UserFilmLikeDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Models.VMs.UserFilmLikeViewModels;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Context;
using MovieWebsite.Infrastructure.Repositories;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.UserFilmLikeServices
{
    public class UserFilmLikeService : IUserFilmLikeService
    {
        private readonly AppDbContext _context;
        private readonly IUserFilmLikeRepo _userFilmLikeRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserFilmLikeService(AppDbContext context, IUserFilmLikeRepo userFilmLikeRepository, IFilmRepository filmRepository, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _userFilmLikeRepository = userFilmLikeRepository;
            _filmRepository = filmRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<UserFilmLikeDto> LikeFilmAsync(string userId, int filmId)
        {
            // Check if the film exists
            var film = await _filmRepository.GetDefault(f => f.Id == filmId);
            

            // Check if the user already liked the film
            var existingLike = await _userFilmLikeRepository.GetDefault(like => like.UserId == userId && like.FilmId == filmId);
            if (existingLike != null)
            {
                
                    

                // Update existing dislike to like
                existingLike.Liked = true;
                existingLike.Disliked = false;
                existingLike.UpdateDate = DateTime.Now;
                await _userFilmLikeRepository.Update(existingLike);
            }
            else
            {
                // Create a new like entry
                var userFilmLike = new UserFilmLike
                {
                    UserId = userId,
                    FilmId = filmId,
                    Liked = true,
                    Disliked = false,
                    CreateDate = DateTime.Now,
                    Status = Status.Active
                };
                await _userFilmLikeRepository.Create(userFilmLike);
            }

            return new UserFilmLikeDto { UserId = userId, FilmId = filmId, Liked = true };
        }

        public async Task<UserFilmLikeDto> DislikeFilmAsync(string userId, int filmId)
        {
            // Check if the film exists
            var film = await _filmRepository.GetDefault(f => f.Id == filmId);
            if (film == null)
                throw new Exception("Film not found.");

            // Check if the user already disliked the film
            var existingDislike = await _userFilmLikeRepository.GetDefault(like => like.UserId == userId && like.FilmId == filmId);
            if (existingDislike != null)
            {


                // Update existing like to dislike
                existingDislike.Liked = false;
                existingDislike.Disliked = true;
                existingDislike.UpdateDate = DateTime.Now;
                await _userFilmLikeRepository.Update(existingDislike);
            }
            else
            {
                // Create a new dislike entry
                var userFilmLike = new UserFilmLike
                {
                    UserId = userId,
                    FilmId = filmId,
                    Liked = false,
                    Disliked = true,
                    CreateDate = DateTime.Now,
                    Status = Status.Active
                };
                await _userFilmLikeRepository.Create(userFilmLike);
            }

            return new UserFilmLikeDto { UserId = userId, FilmId = filmId, Disliked = true };
        }

        public async Task<IEnumerable<UserFilmLikeVM>> GetLikedFilmsByUserAsync(string userId)
        {
           var likedFilms= await _userFilmLikeRepository.GetDefaults(like => like.UserId == userId && like.Liked);

            return likedFilms.Select(like => new UserFilmLikeVM
            {
                FilmId = like.FilmId,
                Liked = like.Liked,
                Films = _mapper.Map<List<FilmVM>>(_filmRepository.GetDefaults(f => f.Id == like.FilmId).Result)

            });
        }

        public async Task<IEnumerable<UserFilmLikeVM>> GetDissLikedFilmsByUserAsync(string userId)
        {
            var dislikedFilms = await _userFilmLikeRepository.GetDefaults(like => like.UserId == userId && like.Disliked);

            return dislikedFilms.Select(like => new UserFilmLikeVM
            {
                FilmId = like.FilmId,                
                Disliked = like.Disliked,
                Films = _mapper.Map<List<FilmVM>>(_filmRepository.GetDefaults(f => f.Id == like.FilmId).Result)
            });
        }

        public async Task<bool> HasUserLikedFilmAsync(string userId, int filmId)
        {
            var like = await _userFilmLikeRepository.GetDefault(like => like.UserId == userId && like.FilmId == filmId && like.Liked);
            return like != null;
        }

        public async Task<UserFilmLikeVM> RemoveLikeAsync(string userId, int filmId)
        {
            var like = await _userFilmLikeRepository.GetDefault(like => like.UserId == userId && like.FilmId == filmId && like.Liked);
            

            like.Liked = false;
            like.UpdateDate = DateTime.Now;
            await _userFilmLikeRepository.Update(like);

            return new UserFilmLikeVM { FilmId = filmId, Liked = false };
        }

        public async Task<UserFilmLikeVM> RemoveDislikeAsync(string userId, int filmId)
        {
            var dislike = await _userFilmLikeRepository.GetDefault(like => like.UserId == userId && like.FilmId == filmId && like.Disliked);
            

            dislike.Disliked = false;
            dislike.UpdateDate = DateTime.Now;
            await _userFilmLikeRepository.Update(dislike);

            return new UserFilmLikeVM { FilmId = filmId, Disliked = false };
        }
        public async Task<bool> HasUserDislikedFilmAsync(string userId, int filmId)
        {
            var dislike = await _userFilmLikeRepository.GetDefault(like => like.UserId == userId && like.FilmId == filmId && like.Disliked);
            return dislike != null;
        }
    }
}

