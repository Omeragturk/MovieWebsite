using MovieWebsite.Application.Models.VMs.FilmVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.VMs.UserFilmLikeViewModels
{
    public class UserFilmLikeVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FilmId { get; set; }
        public bool Disliked { get; set; }
        public List<FilmVM>? Films { get; set; }
       
    }
}
