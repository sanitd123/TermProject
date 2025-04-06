using Microsoft.AspNetCore.Mvc;

namespace RestaurantReviewCoreMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
