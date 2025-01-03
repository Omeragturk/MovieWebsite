using MovieWebsite.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieWebsite.Domain.Entities
{
    public class User : IdentityUser, IBaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImagePath { get; set; }
        
        [NotMapped]
        public IFormFile? Image { get; set; }
        public ICollection<UserFilmLike> UserFilmLikes { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get ; set; }
        public DateTime? DeleteDate { get ; set ; }
        public Status Status { get ; set; }
    }
}
