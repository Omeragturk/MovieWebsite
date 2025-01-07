using MovieWebsite.Application.Models.VMs.ReviewVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.VMs.FilmVMs
{
    public class FilmDetailVM
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string GenreName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImagePath { get; set; }

        public List<ReviewVM> Reviews { get; set; }
    }
}
