
using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.DTOs.GenreDto
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "Please enter a category name!")]
        [MinLength(3, ErrorMessage = "Category name must be at least 3 characters!")]        
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Category name must be a character!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        
    }
}
