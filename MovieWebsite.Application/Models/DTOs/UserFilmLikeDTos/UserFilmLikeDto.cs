using MovieWebsite.Application.Models.DTOs.FilmDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.DTOs.UserFilmLikeDTos
{
    public class UserFilmLikeDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FilmId { get; set; }
        public bool Disliked { get; set; }  
        public List<CreateFilmDTO> Films { get; set; }
        
    }
}
