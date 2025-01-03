using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Infrastructure.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {

        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserFilmLike> UserFilmLikes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "Admin",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = new Guid().ToString(),
            },
            new IdentityRole
            {
                Id = "Member",
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = new Guid().ToString(),
            });
            // Seed Genres
            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action", Description = "Action genre" },
                new Genre { Id = 2, Name = "Comedy", Description = "Comedy genre" },
                new Genre { Id = 3, Name = "Drama", Description = "Drama genre" },
                new Genre { Id = 4, Name = "Horror", Description = "Horror genre" },
                new Genre { Id = 5, Name = "Romance", Description = "Romance genre" },
                new Genre { Id = 6, Name = "Sci-Fi", Description = "Sci-Fi genre" }
            );

            // Seed Films
            builder.Entity<Film>().HasData(
                new Film
                {
                    Id = 1,
                    Title = "The Last Stand",
                    Description = "The leader of a drug cartel busts out of a courthouse and speeds to the Mexican border, where the only thing in his path is a sheriff and his inexperienced staff.",
                    CreateDate = DateTime.Now,
                    GenreId = 1,                    
                    ImagePath = "/moviehunter/css/images/movie1.jpg"
                },
                new Film
                {
                    Id = 2,
                    Title = "Spider Man 2",
                    Description = " Peter Parker is beset with troubles in his failing personal life as he battles a brilliant scientist named Doctor Otto Octavius.",
                    CreateDate = DateTime.Now,
                    GenreId = 1,
                    ImagePath = "/moviehunter/css/images/movie2.jpg"
                },
                new Film
                {
                    Id = 3,
                    Title = "Spider Man 3",
                    Description = "A strange black entity from another world bonds with Peter Parker and causes inner turmoil as he contends with new villains, temptations, and revenge.",
                    CreateDate = DateTime.Now,
                    GenreId = 1,
                    ImagePath = "/moviehunter/css/images/movie3.jpg"

                },
                new Film
                {
                    Id = 4,
                    Title = "Valkiyrie",
                    Description = "A dramatization of the July 20, 1944 assassination and political coup plot by desperate renegade German Army officers",
                    CreateDate = DateTime.Now,
                    GenreId = 3,
                    ImagePath = "/moviehunter/css/images/movie4.jpg"

                },
                new Film
                {
                    Id = 5,
                    Title = "Gladiator",
                    Description = " A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him",
                    CreateDate = DateTime.Now,
                    GenreId = 3,
                    ImagePath = "/moviehunter/css/images/movie5.jpg"

                },
                new Film
                {
                    Id = 6,
                    Title = "Ice Age",
                    Description = " Set during the Ice Age, a sabertooth tiger, a sloth, and a wooly mammoth find a lost human infant, and they try to return him to his tribe.",
                    CreateDate = DateTime.Now,
                    GenreId = 2,
                    ImagePath = "/moviehunter/css/images/movie6.jpg"

                },
                new Film
                {
                    Id = 7,
                    Title = "Transformers",
                    Description = "An ancient struggle between two Cyber" ,
                    CreateDate = DateTime.Now,
                    GenreId = 6,
                    ImagePath = "/moviehunter/css/images/movie7.jpg"

                },
                new Film
                {
                    Id = 8,
                    Title = "Magneto",
                    Description = " In 1962, the United States government enlists the help of Mutants with superhuman abilities to stop a malicious dictator who is determined to start World War III.",
                    CreateDate = DateTime.Now,
                    GenreId = 6,
                    ImagePath = "/moviehunter/css/images/movie8.jpg"

                },
                new Film
                {
                    Id = 9,
                    Title = "Kung Fu Panda",
                    Description = " The Dragon Warrior has to clash against the savage Tai Lung as China's fate hangs in the balance: However, the Dragon Warrior mantle is supposedly mistaken to be bestowed upon an obese panda who is a tyro in martial arts.",
                    CreateDate = DateTime.Now,
                    GenreId = 2,
                    ImagePath = "/moviehunter/css/images/movie9.jpg"

                },
                new Film
                {
                    Id = 10,
                    Title = "Eagle Eye",
                    Description = " Jerry and Rachel are two strangers thrown together by a mysterious phone call from a woman they have never met. Threatening their lives and family, she pushes Jerry and Rachel into a series of increasingly dangerous situations, using the technology of everyday life to track and control their every move.",
                    CreateDate = DateTime.Now,
                    GenreId = 1,
                    ImagePath = "/moviehunter/css/images/movie10.jpg"

                },
                new Film
                {
                    Id = 11,
                    Title = "Angels and Demons",
                    Description = "Harvard symbologist Robert Langdon works with a nuclear physicist to solve a murder and prevent a terrorist act against the Vatican during one of the significant events within the church.", 
                    CreateDate = DateTime.Now,
                    GenreId= 2,
                    ImagePath = "/moviehunter/css/images/movie12.jpg"

                },
                new Film
                {
                    Id= 12,
                    Title= "House",
                    Description= "In rural Alabama, two couples find themselves in a fight for survival. Running from a maniac (The Tin Man) bent on killing them, they flee deep into the woods and seek refuge in a house.",
                    CreateDate = DateTime.Now,
                    GenreId= 3,
                    ImagePath = "/moviehunter/css/images/movie13.jpg"

                }




            );

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JI3UVS4;Database=FilmRecomendedApp;Uid=sa;Pwd=123");

        }

    }
}
