using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.EntityTypeConfigurations
{
    public class FilmConfig:BaseEntityConfig<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            //builder.HasKey(f => f.Id);

            //builder.HasOne(f => f.Genre)
            //    .WithMany(g => g.Films) // Bir tür birden fazla filme sahip olabilir
            //    .HasForeignKey(f => f.GenreId)
            //    .IsRequired();

            //builder.HasMany(f => f.UserFilmLikes) // Bir film birden fazla kullanıcı tarafından beğenilebilir
            //    .WithOne(ufl => ufl.Film)
            //    .HasForeignKey(ufl => ufl.FilmId);

            // Gerekirse ekstra yapılandırmalar yapabilirsiniz
        }
    }
}
