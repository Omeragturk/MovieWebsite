using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.EntityTypeConfigurations
{
    public class UserFilmLikeConfig :BaseEntityConfig<UserFilmLike>
    {
        public void Configure(EntityTypeBuilder<UserFilmLike> builder)
        {           
            // User ile ilişki
            //builder.HasOne(ufl => ufl.User)
            //       .WithMany(u => u.UserFilmLikes)
            //       .HasForeignKey(ufl => ufl.UserId);

            //// Film ile ilişki
            //builder.HasOne(ufl => ufl.Film)
            //       .WithMany(f => f.UserFilmLikes)
            //       .HasForeignKey(ufl => ufl.FilmId);

            base.Configure(builder);

        }
    }
}
