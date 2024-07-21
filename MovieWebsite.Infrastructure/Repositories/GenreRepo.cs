using Microsoft.EntityFrameworkCore;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.Repositories
{
    public class GenreRepo : BaseRepo<Genre>, IGenreRepository
    {
        public GenreRepo(AppDbContext context) : base(context)
        {
        }
    }
}
