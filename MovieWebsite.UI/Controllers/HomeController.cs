using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Application.Services.GenreServices;
using MovieWebsite.Domain.Interfaces;

namespace MovieWebsite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFilmService   _filmService;
        private readonly IGenreService _genreService;

        public HomeController(IFilmService filmService, IGenreService genreService)
        {
            _filmService = filmService;
            _genreService = genreService;
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
            if (filmDetail == null)
            {
                return NotFound();
            }
            return View(filmDetail);
        }
    }
}
