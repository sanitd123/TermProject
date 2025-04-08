using Microsoft.AspNetCore.Mvc;
using RestaurantReviewCoreMVC.Models;

namespace RestaurantReviewCoreMVC.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            RestaurantDB restaurantDB = new RestaurantDB();
            restaurantDB.InsertReview(review);
            return View("InsertReview");
        }
    }
}
