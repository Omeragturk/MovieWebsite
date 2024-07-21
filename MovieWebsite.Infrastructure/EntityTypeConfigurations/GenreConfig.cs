using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.EntityTypeConfigurations
{
    public class GenreConfig<T> :BaseEntityConfig<Genre> where T : class
    {
        public  void Configure(EntityTypeBuilder<Genre> builder)
        {
            
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);

        }
    }
}
