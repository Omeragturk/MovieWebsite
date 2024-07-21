using ETicaretPlatformu.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.DTOs.FilmDTos
{
    public class CreateFilmDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [PictureFileExtensionAttiribute]
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile UpLoadPath { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public int GenreId { get; set; }
    }
}
