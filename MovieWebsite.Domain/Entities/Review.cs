using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Domain.Entities
{
    public class Review : IBaseEntity
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }


        public DateTime CreateDate { get ; set; }
        public DateTime? UpdateDate { get ; set ; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
}
