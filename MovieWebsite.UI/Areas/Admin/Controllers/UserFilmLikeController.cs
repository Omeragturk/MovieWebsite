using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Services.UserFilmLikeServices;

namespace MovieWebsite.UI.Areas.Admin.Controllers
{
    public class UserFilmLikeController : Controller
    {
        private readonly IUserFilmLikeService _userFilmLikeService;
        public UserFilmLikeController(IUserFilmLikeService userFilmLikeService)
        {
            _userFilmLikeService = userFilmLikeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userFilmLikeService.GetLikedFilmsByUserAsync(User.Identity.Name));
        }
        public async Task<IActionResult> Like(int filmId)
        {
            await _userFilmLikeService.LikeFilmAsync(User.Identity.Name, filmId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Dislike(int filmId)
        {
            await _userFilmLikeService.DislikeFilmAsync(User.Identity.Name, filmId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetLikedFilms()
        {
            return View(await _userFilmLikeService.GetLikedFilmsByUserAsync(User.Identity.Name));
        }

    }
}
