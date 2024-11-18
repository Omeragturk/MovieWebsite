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

            var result = await _userFilmLikeService.LikeFilmAsync(userId, filmId);
            if (result == null)
            {
                TempData["Error"] = "Film beğenilemedi. Film veya kullanıcı bulunamadı.";
            }
            else
            {
                TempData["Success"] = "Film başarıyla beğenildi.";
            }

            // Yönlendirme işlemini, Admin/UserFilmLikeController/LikedFilms olarak yapıyoruz
            return RedirectToAction("LikedFilms", "UserFilmLike");

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

            await _userFilmLikeService.DislikeFilmAsync(userId, filmId);
            TempData["Success"] = "Film beğenmekten vazgeçildi.";

            
            return RedirectToAction("DisLikedFilms", "UserFilmLike");
        }



        public async Task<IActionResult> LikedFilms()
        {
            var userId = _userManager.GetUserId(User);

            var likedFilms = await _userFilmLikeService.GetLikedFilmsByUserAsync(userId);
            if (likedFilms == null || !likedFilms.Any())
            {
                TempData["Error"] = "Henüz hiçbir film beğenmediniz.";
                return View();
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
                return View();
            }

            return View(dislikedFilms);

        }
    }
}
