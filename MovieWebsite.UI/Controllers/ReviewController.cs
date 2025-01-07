using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWebsite.Application.Models.DTOs.ReviewDtos;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Application.Services.ReviewServices;
using MovieWebsite.Application.Services.UserServices;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Interfaces;

namespace MovieWebsite.UI.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IFilmService _filmService;

        public ReviewController(IReviewService reviewService, UserManager<User> userManager, IFilmService filmService, IUserService userService)
        {
            _reviewService = reviewService;
            _userManager = userManager;
            _filmService = filmService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllReviews();           
            return View(reviews);

        }

        public async Task<IActionResult> Details(int id)
        {
            var review = await _reviewService.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Film beğenmek için giriş yapmalısınız.";
                return RedirectToAction("Login", "Account");
            }

            // Burada await doğru bir şekilde kullanılıyor mu kontrol edin.
            var films = await _filmService.GetAllFilms();
            ViewBag.Films = new SelectList(films, "Id", "Title");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewDto model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Yorum eklemek için giriş yapmalısınız.";
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcı kimliği
            model.UserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(model.UserId))
            {
                TempData["Error"] = "Kullanıcı kimliği alınamadı.";
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reviewService.Create(model);
                    TempData["Success"] = "Yorum başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Yorum eklenirken bir hata oluştu: {ex.Message}";
                }
            }

            // Hatalı model durumunda filmleri tekrar doldur
            var films = await _filmService.GetAllFilms();
            ViewBag.Films = new SelectList(films, "Id", "Title");

            return View(model);
        }






        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewService.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            // Get the list of films
            var films = await _filmService.GetAllFilms();
            ViewBag.Films = new SelectList(films, "Id", "Title");

            return View(review);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateReviewDto updateReviewDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // Debug or log the errors
                }

                var films = await _filmService.GetAllFilms();
                ViewBag.Films = new SelectList(films, "Id", "Title");
                return View(updateReviewDto);
            }


            await _reviewService.Update(updateReviewDto);

            // Assuming the update is always successful
            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.Delete(id);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> RecentReviews(int filmId, int count)
        {
            var reviews = await _reviewService.GetRecentReviews(filmId, count);
            return View(reviews);

        }

        public async Task<IActionResult> AverageRating(int filmId)
        {
            var averageRating = await _reviewService.GetAverageRatingByFilmId(filmId);
            return View(averageRating);

        }

        public async Task<IActionResult> UserReviews(string userId)
        {
            var reviews = await _reviewService.GetReviewsByUserId(userId);
            return View(reviews);

        }
    }
}
