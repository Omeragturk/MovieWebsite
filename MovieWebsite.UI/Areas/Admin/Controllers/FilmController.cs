using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWebsite.Application.Models.DTOs.FilmDTos;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Application.Services.GenreServices;
using System.Threading.Tasks;

namespace MovieWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IGenreService _genreService;

        public FilmController(IFilmService filmService, IGenreService genreService)
        {
            _filmService = filmService;
            _genreService = genreService;
        }

        
        public async Task<IActionResult> Index()
        {
            var films = await _filmService.GetAllFilms();
            return View(films);
        }
      
        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFilmDTO filmDto)
        {
            if(filmDto.UpLoadPath != null)
            {

                filmDto.ImagePath = await _filmService.SaveFile(filmDto.UpLoadPath);
            }
            if (ModelState.IsValid)
            {
                await _filmService.Create(filmDto);
                ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name");
                return RedirectToAction(nameof(Index));
            }
           
            return View(filmDto);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            // Convert FilmVM to UpdateFilmDto
            var updateFilmDto = new UpdateFilmDto
            {
                Id = film.Id,
                Title = film.Title,
                Description = film.Description,
                GenreId = film.GenreId,
                GenreName = film.GenreName,
                ImagePath = film.ImagePath,
            };

            ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name", film.GenreId);
            return View(updateFilmDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateFilmDto filmDto)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name", filmDto.GenreId);
                return View(filmDto);
            }
            else
            {
                await _filmService.UpdateFilm(filmDto);
                TempData["Success"] = "Film updated successfully.";
                return RedirectToAction(nameof(Index));
            }

        }



        
        public async Task<IActionResult> Delete(int id)
        {
            await _filmService.DeleteFilmAsync(id);
            TempData["Success"] = "Film deleted successfully.";
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Details(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }
    }
}
