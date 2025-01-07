using Microsoft.AspNetCore.Http;
using MovieWebsite.Application.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.VMs.FilmVMs
{
    public class FilmVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [PictureFileExtensionAttiribute]
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? UpLoadPath { get; set; }
        public string GenreName { get; set; }
        public int GenreId { get; set; }
        public double Rating { get; set; }
    }
}
