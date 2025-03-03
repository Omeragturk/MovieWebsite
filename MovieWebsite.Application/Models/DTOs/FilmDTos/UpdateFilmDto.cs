﻿using Microsoft.AspNetCore.Http;
using MovieWebsite.Application.Extensions;
using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.DTOs.FilmDTos
{
    public class UpdateFilmDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [PictureFileExtensionAttiribute]
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? UpLoadPath { get; set; }
        public int GenreId { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status=> Status.Modified;

        public string GenreName { get; set; }
    }
}
