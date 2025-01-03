using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Application.Models.DTOs.ReviewDtos;
using MovieWebsite.Application.Models.VMs.ReviewVMs;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using MovieWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IFilmRepository filmRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _filmRepository = filmRepository;
            _userRepository = userRepository;
        }
        public async Task Create(CreateReviewDto model)
        {
            var film = await _filmRepository.GetDefaults(f => f.Id == model.FilmId);
            if (film == null)
                throw new Exception("Film bulunamadı.");

            var existingReviews = await _reviewRepository.GetDefaults(r => r.FilmId == model.FilmId && r.UserId == model.UserId);
            if (existingReviews.Any())
                throw new Exception("Kullanıcı bu filme zaten yorum yaptı.");

            var review = new Review
            {
                FilmId = model.FilmId,
                UserId = model.UserId,
                Rating = model.Rating,
                Text = model.Text,
                CreateDate = DateTime.Now,
                Status = Status.Active
            };

            await _reviewRepository.Create(review);
        }




        public async Task Delete(int id)
        {

            var review = await _reviewRepository.GetDefault(r => r.Id == id);
            if (review == null)
            {
                throw new Exception("Review not found");
            }
            review.Status = Status.Passive;
            review.UpdateDate = DateTime.Now;
            await _reviewRepository.Update(review);


        }

        public async Task<List<ReviewVM>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetFilteredList(
        select: r => new ReviewVM
        {
            Id = r.Id,
            Rating = r.Rating,
            Text = r.Text,
            FilmId = r.FilmId,
            Film = r.Film,
            UserId = r.UserId,
            User = r.User,
            CreateDate = r.CreateDate,
            UpdateDate = r.UpdateDate,
            DeleteDate = r.DeleteDate,
            Status = r.Status
        },
        where: r => r.Status == Status.Active,
        include: q => q.Include(r => r.Film).Include(r => r.User)
    );

            return reviews;



        }

        public async Task<double> GetAverageRatingByFilmId(int filmId)
        {
            var reviews = await _reviewRepository.GetDefaults(r => r.FilmId == filmId);
            if (reviews.Count == 0)
            {
                return 0;
            }
            return reviews.Average(r => r.Rating);


        }

        public async Task<UpdateReviewDto> GetById(int id)
        {
            var review = await _reviewRepository.GetDefault(r => r.Id == id);
            if (review == null)
            {
                throw new Exception("Review not found");
            }
            return _mapper.Map<UpdateReviewDto>(review);

        }

        public async Task<List<ReviewVM>> GetRecentReviews(int filmId, int count)
        {
            var reviews = await _reviewRepository.GetDefaults(r => r.FilmId == filmId);
            return _mapper.Map<List<ReviewVM>>(reviews.OrderByDescending(r => r.CreateDate).Take(count));

        }

        public async Task<int> GetReviewCountByFilmId(int filmId)
        {
            var reviews = await _reviewRepository.GetDefaults(r => r.FilmId == filmId);
            return reviews.Count;

        }

        public async Task<List<ReviewVM>> GetReviewsByFilmId(int filmId)
        {
            var reviews = await _reviewRepository.GetDefaults(r => r.FilmId == filmId);
            return _mapper.Map<List<ReviewVM>>(reviews);


        }

        public async Task<List<ReviewVM>> GetReviewsByRating(int rating)
        {
            var reviews = await _reviewRepository.GetDefaults(r => r.Rating == rating);
            return _mapper.Map<List<ReviewVM>>(reviews);

        }

        public async Task<List<ReviewVM>> GetReviewsByUserId(string userId)
        {
            var reviews = await _reviewRepository.GetDefaults(r => r.UserId == userId);
            return _mapper.Map<List<ReviewVM>>(reviews);

        }

        public async Task Restore(int id)
        {
            var review = await _reviewRepository.GetDefault(r => r.Id == id);
            if (review == null)
            {
                throw new Exception("Review not found");
            }
            review.Status = Status.Active;
            review.UpdateDate = DateTime.Now;
            await _reviewRepository.Update(review);

        }

        public async Task Update(UpdateReviewDto model)
        {
            var review = await _reviewRepository.GetDefault(r => r.Id == model.Id);
            if (review == null)
            {
                throw new Exception("Review not found");
            }
            review.Rating = model.Rating;
            review.Text = model.Text;
            review.UpdateDate = DateTime.Now;
            await _reviewRepository.Update(review);


        }
    }
}
