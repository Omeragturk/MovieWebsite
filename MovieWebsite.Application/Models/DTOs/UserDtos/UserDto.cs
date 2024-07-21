using MovieWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Models.DTOs.UserDtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
    }
}
