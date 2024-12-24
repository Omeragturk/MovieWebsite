using ETicaretPlatformu.Application.Models.DTOs.CatagoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Models.DTOs.GenreDto;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Application.Services.GenreServices;

namespace MovieWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IFilmService _filmService;

        public GenreController(IGenreService genreService, IFilmService filmService)
        {
            _genreService = genreService;
            _filmService = filmService;
        }




        [AllowAnonymous]
        public async Task<IActionResult> Index(int? genreId)
        {
            var genres = await _genreService.GetGenres();
            ViewBag.Genres = genres;

            if (genreId.HasValue)
            {
                var movies = await _filmService.GetFilmsByGenreId(genreId.Value);
                ViewBag.Movies = movies;
            }
            else
            {
                ViewBag.Movies = new List<FilmVM>();
            }

            return View(genres);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDto genreDto)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Create(genreDto);
                return RedirectToAction("Index");
            }
            return View(genreDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _genreService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateGenreDto genreDto)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Update(genreDto);
                return RedirectToAction("Index");
            }
            return View(genreDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> ListGenres()
        {
            var genres = await _genreService.GetGenres();
            return View(genres);
        }
        
    }
}
