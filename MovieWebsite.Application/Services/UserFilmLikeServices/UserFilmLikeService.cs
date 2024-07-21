using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Application.Models.DTOs.UserFilmLikeDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Context;
using MovieWebsite.Infrastructure.Repositories;
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

        public async Task DislikeFilmAsync(string userId, int filmId)
        {
            
            var userFilmLike = await _userFilmLikeRepository.GetDefault(uf => uf.UserId == userId && uf.FilmId == filmId);
            if (userFilmLike != null)
            {
                
                await _userFilmLikeRepository.Delete(userFilmLike);
            }
        }

        public async Task<List<FilmVM>> GetLikedFilmsByUserAsync(string userId)
        {
           var likedFilms = await _userFilmLikeRepository.GetLikedFilmsByUserId(userId);
            return _mapper.Map<List<FilmVM>>(likedFilms);
        }

        public async Task<bool> HasUserLikedFilmAsync(string userId, int filmId)
        {
            
            return await _userFilmLikeRepository.Any(uf => uf.UserId == userId && uf.FilmId == filmId);
        }

        public async Task<UserFilmLikeDto> LikeFilmAsync(string userId, int filmId)
        {
            
            var existingLike = await _userFilmLikeRepository.GetDefault(uf => uf.UserId == userId && uf.FilmId == filmId);
            if (existingLike != null)
            {
                
                return _mapper.Map<UserFilmLikeDto>(existingLike);
            }

            
            var user = await _userRepository.GetDefault(u => u.Id == userId);
            var film = await _filmRepository.GetDefault(f => f.Id == filmId);

            if (user == null || film == null)
            {
                // User or film not found, return null
                return null;
            }

            
            var userFilmLike = new UserFilmLike
            {
                UserId = userId,
                FilmId = filmId,
                User = user,
                Film = film,
                CreateDate = DateTime.Now
            };

            
            await _userFilmLikeRepository.Create(userFilmLike);

            
            return _mapper.Map<UserFilmLikeDto>(userFilmLike);
        }
    }
}

