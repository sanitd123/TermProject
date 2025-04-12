using Microsoft.AspNetCore.Mvc;

namespace RestaurantReviewCoreMVC.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
