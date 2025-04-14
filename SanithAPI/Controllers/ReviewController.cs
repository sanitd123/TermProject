using Microsoft.AspNetCore.Mvc;

namespace SanithAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Review")]
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<Review> 
    }
}
