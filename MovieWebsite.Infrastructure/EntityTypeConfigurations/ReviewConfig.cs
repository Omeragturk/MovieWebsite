using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.EntityTypeConfigurations
{
    public class ReviewConfig:BaseEntityConfig<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(500);
            builder.HasOne(x => x.Film).WithMany(x => x.Reviews).HasForeignKey(x => x.FilmId);
            builder.HasOne(x => x.User).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId);
        }

    }
}
