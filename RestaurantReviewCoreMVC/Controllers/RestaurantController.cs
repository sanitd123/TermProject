using Microsoft.AspNetCore.Mvc;
using RestaurantReviewCoreMVC.Models;
using System.Net;
using System.Text.Json;

namespace RestaurantReviewCoreMVC.Controllers
{
    public class RestaurantController : Controller
    {
        string webApiUrl = "https://localhost:7110/api/Review/";
        // string webApiUrl = "https:cis-iis2.temple.edu/Spring2025/CIS3342_tui96569/TermProject/api/Review/";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test() 
        {
            TempData["reservationID"] = 1;
            HttpContext.Session.SetInt32("accountID", 2);
            TempData["restaurantID"] = 1;
            return RedirectToAction("Reservation");
        }

        [HttpGet]
        public IActionResult Review()
        {
            if (TempData.Peek("reviewID") != null) // if redirected with reviewID update
            {
                WebRequest request = WebRequest.Create(webApiUrl + "GetReview/" + Convert.ToInt32(TempData["reviewID"]));
                WebResponse response = request.GetResponse();

                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                Review review = JsonSerializer.Deserialize<Review>(data);

                return View("Review", review);
            }
            else
            {
                Review review = new Review();
                review.AccountID = (int)HttpContext.Session.GetInt32("accountID");
                review.RestaurantID = Convert.ToInt32(TempData["restaurantID"]);
                return View("Review", review); // no reviewID insert
            }
        }

        [HttpPost]
        public IActionResult Review(Review review)
        {
            if (!ModelState.IsValid) // input validation does not pass
            {
                return View("Review", review);
            }

            string jsonReview = JsonSerializer.Serialize(review);

            if (review.ReviewID < 1)
            {
                try
                {
                    WebRequest request = WebRequest.Create(webApiUrl + "InsertReview");
                    request.Method = "POST";
                    request.ContentLength = jsonReview.Length;
                    request.ContentType = "application/json";

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(jsonReview);
                    writer.Flush();
                    writer.Close();

                    request.GetResponse();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    WebRequest request = WebRequest.Create(webApiUrl + "UpdateReview");
                    request.Method = "PUT";
                    request.ContentLength = jsonReview.Length;
                    request.ContentType = "application/json";

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(jsonReview);
                    writer.Flush();
                    writer.Close();

                    request.GetResponse();
                }
                catch (Exception ex)
                {

                }
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
            else
            {
                Reservation reservation = new Reservation();
                reservation.RestaurantID = Convert.ToInt32(TempData["restaurantID"]);
                return View("Reservation", reservation); // no reservationID insert
            }
        }

        [HttpPost]
        public IActionResult Reservation(Reservation reservation)
        {
            if (!ModelState.IsValid) // input validation does not pass
            {
                return View("Reservation", reservation);
            }

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
