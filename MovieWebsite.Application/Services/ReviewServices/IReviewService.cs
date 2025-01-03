using MovieWebsite.Application.Models.DTOs.ReviewDtos;
using MovieWebsite.Application.Models.VMs.ReviewVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.ReviewServices
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllReviews();
        Task<List<ReviewVM>> GetReviewsByFilmId(int filmId);
        Task<List<ReviewVM>> GetReviewsByRating(int rating);
        Task<List<ReviewVM>> GetRecentReviews(int filmId, int count);
        Task<List<ReviewVM>> GetReviewsByUserId(string userId);
        Task<int> GetReviewCountByFilmId(int filmId);
        Task<double> GetAverageRatingByFilmId(int filmId);

        Task Create(CreateReviewDto model);
        Task Update(UpdateReviewDto model);
        Task Delete(int id);
        
        Task<UpdateReviewDto> GetById(int id);
    }
}
