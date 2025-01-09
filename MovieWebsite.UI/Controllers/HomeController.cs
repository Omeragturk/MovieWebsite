using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Models.VMs.ReviewVMs;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Application.Services.GenreServices;
using MovieWebsite.Application.Services.ReviewServices;
using MovieWebsite.Application.Services.UserFilmLikeServices;
using MovieWebsite.Application.Services.UserServices;
using MovieWebsite.Domain.Interfaces;

namespace MovieWebsite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IGenreService _genreService;
        private readonly IUserService _userService;
        private readonly IUserFilmLikeService _userFilmLikeService;
        private readonly IReviewService _reviewService;

        public HomeController(IFilmService filmService, IGenreService genreService, IUserService userService, IUserFilmLikeService userFilmLikeService,IReviewService reviewService)
        {
            _filmService = filmService;
            _genreService = genreService;
            _userService = userService;
            _userFilmLikeService = userFilmLikeService;
            _reviewService = reviewService;
        }
        [AllowAnonymous]
        public async Task<ActionResult> Index(int? genreId)
        {
            var films = await _filmService.GetAllFilms();
            ViewBag.Genres = await _genreService.GetGenres();

            if (genreId.HasValue)
            {
                films = films.Where(x => x.GenreId == genreId).ToList();
            }

            
            var filmVMs = new List<FilmVM>();
            foreach (var film in films)
            {
                var rating = await _reviewService.GetAverageRatingByFilmId(film.Id); // Rating'i al
                filmVMs.Add(new FilmVM
                {
                    Id = film.Id,
                    Title = film.Title,
                    Description = film.Description,
                    ImagePath = film.ImagePath,
                    GenreName = film.GenreName,
                    GenreId = film.GenreId,
                    Rating = rating 
                });
            }

            return View(filmVMs);


        }

        [Authorize]
        public async Task<IActionResult> GetFilmDetails(int id)
        {
            // Film detaylarını al
            var filmDetail = await _filmService.GetFilmByIdAsync(id);
            if (filmDetail == null)
            {
                return NotFound("Film bulunamadı.");
            }

            // Giriş yapan kullanıcıyı al
            var currentUser = await _userService.GetByUserName(User.Identity.Name);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            // Kullanıcı like/dislike durumu
            var userLiked = await _userFilmLikeService.HasUserLikedFilmAsync(currentUser.Id, id);
            var userDisliked = await _userFilmLikeService.HasUserDislikedFilmAsync(currentUser.Id, id);

            // Filmle ilgili tüm yorumları al ve kullanıcı bilgilerini ekle
            var reviews = await _reviewService.GetReviewsByFilmId(id);

            // ViewModel'e veri taşı
            var film = new FilmDetailVM
            {
                FilmId = filmDetail.Id,
                Title = filmDetail.Title,
                Description = filmDetail.Description,
                ImagePath = filmDetail.ImagePath,
                GenreName = filmDetail.GenreName,
                Liked = userLiked,
                Disliked = userDisliked,
                Reviews = reviews

            };

            return View(film);
        }

    }
}
