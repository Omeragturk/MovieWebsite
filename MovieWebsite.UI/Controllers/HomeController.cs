using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Application.Services.GenreServices;
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

        public HomeController(IFilmService filmService, IGenreService genreService, IUserService userService, IUserFilmLikeService userFilmLikeService)
        {
            _filmService = filmService;
            _genreService = genreService;
            _userService = userService;
            _userFilmLikeService = userFilmLikeService;
        }
        public async Task<ActionResult> Index(int? genreId)
        {
            var films = await _filmService.GetAllFilms();
            ViewBag.Genres = await _genreService.GetGenres();

            if (genreId.HasValue)
            {
                films = films.Where(x => x.GenreId == genreId).ToList();
            }
            return View(films);


        }


        public async Task<IActionResult> GetFilmDetails(int id)
        {
            var filmDetail = await _filmService.GetFilmByIdAsync(id);
            var userId = await _userService.GetByUserName(User.Identity.Name);
            var userLiked = await _userFilmLikeService.HasUserLikedFilmAsync(userId.Id, id);
            var userDisliked = await _userFilmLikeService.HasUserDislikedFilmAsync(userId.Id, id);
            var film = new FilmDetailVM
            {
                FilmId = filmDetail.Id,
                Title = filmDetail.Title,
                Description = filmDetail.Description,
                ImagePath = filmDetail.ImagePath,
                GenreName = filmDetail.GenreName,
                Liked = userLiked,
                Disliked = userDisliked
            };
            return View(film);
        }
    }
}
