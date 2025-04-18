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
        public IActionResult Test() 
        {
            TempData["reviewID"] = 1;
            HttpContext.Session.SetInt32("accountID", 2);
            TempData["restaurantID"] = 1;
            return RedirectToAction("Review");
        }

        [HttpGet]
        public IActionResult Review()
        {
            if (TempData.Peek("reviewID") != null) // if redirected with reviewID update
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                Review review = restaurantDB.GetReview(Convert.ToInt32(TempData["reviewID"]));
                return View("Review", review);
            }

            return View("Review", new Review()); // no reviewID insert
        }

        [HttpPost]
        public IActionResult Review(Review review)
        {
            if (!ModelState.IsValid) // input validation does not pass
            {
                TempData.Keep();
                return View("Review", review);
            }

            review.AccountID = HttpContext.Session.GetInt32("accountID").GetValueOrDefault();
            review.RestaurantID = Convert.ToInt32(TempData["restaurantID"]);
            RestaurantDB restaurantDB = new RestaurantDB();

            if (review.ReviewID < 1)
            {
                restaurantDB.InsertReview(review);
            }
            else
            {
               restaurantDB.UpdateReview(review);
            }

            return View("Review", review);
        }

        [HttpGet]
        public IActionResult Reservation()
        {
            if (TempData.Peek("reservationID") != null) // if redirected with reservationID update
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                Reservation reservation = restaurantDB.GetReservation(Convert.ToInt32(TempData["reservationID"]));
                return View("Reservation", reservation);
            }

            return View("Reservation", new Reservation()); // no reservationID insert
        }

        [HttpPost]
        public IActionResult Reservation(Reservation reservation)
        {
            if (!ModelState.IsValid) // input validation does not pass
            {
                TempData.Keep();
                return View("Reservation", reservation);
            }

            reservation.ReservationID = Convert.ToInt32(TempData["restaurantID"]);
            RestaurantDB restaurantDB = new RestaurantDB();

            if (reservation.ReservationID < 1)
            { 
                restaurantDB.InsertReservation(reservation); // insert if reservationID is not provided
            }
            else
            {
                restaurantDB.UpdateReservation(reservation); // update if reservationID is provided
            }

            return View("Reservation", reservation);
        }
    }
}
