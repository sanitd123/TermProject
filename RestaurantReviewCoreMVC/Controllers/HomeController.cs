using Microsoft.AspNetCore.Mvc;
using RestaurantReviewCoreMVC.Models;
using System.Diagnostics;

namespace RestaurantReviewCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestReservation()
        {
            RestaurantDB restaurantDB = new RestaurantDB();
            Reservation reservation = restaurantDB.GetReservation(1);
            return RedirectToAction("Test", "Restaurant");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
