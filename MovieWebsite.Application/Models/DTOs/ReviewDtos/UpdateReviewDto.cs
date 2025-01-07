using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.DTOs.ReviewDtos
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Review text is required.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Film selection is required.")]
        public int FilmId { get; set; }

        // These might not be required
        public string UserId { get; set; }
        public Film? Film { get; set; }
        public User? User { get; set; }

        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public Status Status { get; set; } = Status.Modified;
    }
}
