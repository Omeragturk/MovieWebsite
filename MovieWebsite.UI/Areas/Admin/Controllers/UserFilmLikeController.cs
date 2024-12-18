using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Application.Services.UserFilmLikeServices;
using MovieWebsite.Domain.Entities;

namespace MovieWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserFilmLikeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserFilmLikeService _userFilmLikeService;

        public UserFilmLikeController(UserManager<User> userManager, IUserFilmLikeService userFilmLikeService)
        {
            _userManager = userManager;
            _userFilmLikeService = userFilmLikeService;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int filmId)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                TempData["Error"] = "Film beğenmek için giriş yapmalısınız.";
                return RedirectToAction("Login", "Account");
            }

            var userId = _userManager.GetUserId(User);

            var likeCheck = await _userFilmLikeService.HasUserLikedFilmAsync(userId, filmId);
            if (likeCheck)
            {
                TempData["Error"] = "Bu filmi zaten beğendiniz.";
                
            }

            await _userFilmLikeService.LikeFilmAsync(userId, filmId);
            TempData["Success"] = "Film başarıyla beğenildi.";
            return RedirectToAction("LikedFilms");
        }

        [HttpPost]
        public async Task<IActionResult> Dislike(int filmId)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                TempData["Error"] = "Film beğenmekten vazgeçmek için giriş yapmalısınız.";
                
                return RedirectToAction("Login", "Account");
            }

            var userId = _userManager.GetUserId(User);

            var dislikeCheck = await _userFilmLikeService.HasUserLikedFilmAsync(userId, filmId);
            if (!dislikeCheck)
            {
                TempData["Error"] = "Bu filmi zaten beğenmediniz.";
                
            }

            await _userFilmLikeService.DislikeFilmAsync(userId, filmId);
            TempData["Success"] = "Film başarıyla beğenilmedi.";
            return RedirectToAction("DislikedFilms");
        }

        public async Task<IActionResult> LikedFilms()
        {
            var userId = _userManager.GetUserId(User);

            var likedFilms = await _userFilmLikeService.GetLikedFilmsByUserAsync(userId);
            if (likedFilms == null || !likedFilms.Any())
            {
                TempData["Error"] = "Henüz hiçbir film beğenmediniz.";
                
            }

            return View(likedFilms);
        }

        public async Task<IActionResult> DislikedFilms()
        {
            var userId = _userManager.GetUserId(User);

            var dislikedFilms = await _userFilmLikeService.GetDissLikedFilmsByUserAsync(userId);
            if (dislikedFilms == null || !dislikedFilms.Any())
            {
                TempData["Error"] = "Henüz hiçbir film beğenmediniz.";
                
            }

            return View(dislikedFilms);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLike(int filmId)
        {
            var userId = _userManager.GetUserId(User);
            await _userFilmLikeService.RemoveLikeAsync(userId, filmId);
            TempData["Success"] = "Film beğenisi kaldırıldı.";
            return RedirectToAction("LikedFilms");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDislike(int filmId)
        {
            var userId = _userManager.GetUserId(User);
            await _userFilmLikeService.RemoveDislikeAsync(userId, filmId);
            TempData["Success"] = "Film beğenmeme işlemi kaldırıldı.";
            return RedirectToAction("DislikedFilms");
        }
    }
}
