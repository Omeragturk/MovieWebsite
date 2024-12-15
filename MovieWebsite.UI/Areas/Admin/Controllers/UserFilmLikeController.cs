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

    // Eğer kullanıcı filmi zaten beğenmişse hata döndür
    var likeCheck = await _userFilmLikeService.LikeFilmAsync(userId, filmId);
    if (likeCheck != null && likeCheck.Disliked)
    {
        TempData["Error"] = "Bu filmi zaten beğendiniz.";
        return RedirectToAction("LikedFilms", "UserFilmLike");
    }

    // Eğer kullanıcı dislike yapmışsa onu kaldır
    if (likeCheck != null && likeCheck.Disliked)
    {
        await _userFilmLikeService.RemoveDislikeAsync(userId, filmId);
    }

    await _userFilmLikeService.LikeFilmAsync(userId, filmId);
    TempData["Success"] = "Film başarıyla beğenildi.";
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

    // Eğer kullanıcı filmi zaten beğenmemişse hata döndür
    var dislikeCheck = await _userFilmLikeService.LikeFilmAsync(userId, filmId);
    if (dislikeCheck != null && dislikeCheck.Disliked)
    {
        TempData["Error"] = "Bu filmi zaten beğenmediniz.";
        return RedirectToAction("DisLikedFilms", "UserFilmLike");
    }

    // Eğer kullanıcı like yapmışsa onu kaldır
    if (dislikeCheck != null && dislikeCheck.Disliked)
    {
        await _userFilmLikeService.RemoveLikeAsync(userId, filmId);
    }

    await _userFilmLikeService.DislikeFilmAsync(userId, filmId);
    TempData["Success"] = "Film başarıyla beğenilmedi.";
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
