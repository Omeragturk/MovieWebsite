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
    public class CreateReviewDto
    {

        [Range(1, 10, ErrorMessage = "Puan 1 ile 10 arasında olmalıdır.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Yorum alanı boş bırakılamaz.")]
        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Film seçmek zorunludur.")]
        public int FilmId { get; set; }

        public string? UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Status Status { get; set; } = Status.Active;
    }
}
