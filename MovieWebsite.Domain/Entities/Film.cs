using Microsoft.AspNetCore.Http;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Domain.Entities
{
    public class Film:IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile UpLoadPath { get; set; }
        
        public Genre Genre { get; set; }
        public int GenreId { get; set; }

        public ICollection<UserFilmLike> UserFilmLikes { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get ; set ; }
        public DateTime? DeleteDate { get ; set ; }
        public Status Status { get ; set; }
        
    }
}
