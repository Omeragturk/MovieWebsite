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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _filmService.GetAllFilms());
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
            if (ModelState.IsValid)
            {
                if (filmDto.UpLoadPath != null && filmDto.UpLoadPath.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/images", filmDto.UpLoadPath.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await filmDto.UpLoadPath.CopyToAsync(stream);
                    }
                    filmDto.ImagePath = "/images/" + filmDto.UpLoadPath.FileName;
                }

                await _filmService.Create(filmDto);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name");
            return View(filmDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name");
            return View(film);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateFilmDto filmDto, int id)
        {
            if (ModelState.IsValid)
            {
                await _filmService.UpdateFilm(filmDto, id);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = new SelectList(await _genreService.GetGenres(), "Id", "Name");
            return View(filmDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _filmService.DeleteFilmAsync(id);
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
