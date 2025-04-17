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
        [HttpGet]
        public IActionResult Review(int reviewID)
        {
            if (reviewID == -1) // if redirected with reviewID update
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                Review review = restaurantDB.GetReview(reviewID);
                return View("Review", review);
            }
            return View("Review", new Review()); // no reviewID insert
        }
        [HttpPost]
        public IActionResult Review(Review review)
        {
            RestaurantDB restaurantDB = new RestaurantDB();

            if (review.ReviewID == null)
            {
                //restaurantDB.InsertReview(review);
            }
            else
            {
                //restaurantDB.UpdateReview(review);
            }

            return View("Review", review);
        }

        public IActionResult Reservation(int? reservationID)
        {
            if (reservationID.HasValue) // if redirected with reservationID update
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                Reservation reservation = restaurantDB.GetReservation(reservationID);
                return View("Reservation", reservation);
            }

            return View("Reservation", new Reservation()); // no reservationID insert
        }

        [HttpPost]
        public IActionResult Reservation(Reservation reservation)
        {
            RestaurantDB restaurantDB = new RestaurantDB();

            if (reservation.ReservationID == null) 
            { 
                //restaurantDB.InsertReservation(reservation);
            }
            else
            {
                //restaurantDB.UpdateReview(review);
            }

            return View("Reservation", reservation);
        }
    }
}
