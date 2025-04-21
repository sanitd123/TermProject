using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RestaurantReviewCoreMVC.Models;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
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



        [HttpGet("Restaurant/ViewRestaurant/{restaurantID:int}")]
        public IActionResult ViewRestaurant(int restaurantID)
        {


            Console.WriteLine(restaurantID.ToString());
            string url = "https://localhost:7163/api/Restaurant/ViewRestaurant/" + restaurantID;
            WebRequest getRequest = WebRequest.Create(url);
            getRequest.Method = "GET";




            WebResponse response = getRequest.GetResponse();

            Stream theDataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(theDataStream, System.Text.Encoding.UTF8);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
            Restaurant restaurant = JsonSerializer.Deserialize<Restaurant>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View("ViewRestaurant", restaurant);

        }

        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant != null)
                {
                    Console.WriteLine("Rest not null");
                    string json = JsonSerializer.Serialize(restaurant);
                    WebRequest request = WebRequest.Create("https://localhost:7163/api/Restaurant/AddRestaurant");

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();

                    WebResponse response = request.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();


                    if (data == "true")
                    {

                        //success
                    }
                    else if (data == "false")
                    {
                        //error
                    }
                }
                else
                {
                    //no restaurant
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddRestaurant (View): {ex.Message}\n{ex.StackTrace}");
            }

            return View();
        }
        [HttpGet]
        public IActionResult SearchRestaurant()
        {

            List<Restaurant> restaurants = new List<Restaurant>();
            Restaurant rest = new Restaurant();
            Restaurant rest2 = new Restaurant();
            rest2.Title = "TITLE2";
            rest.Title = "TITLE";
            restaurants.Add(rest);
            restaurants.Add(rest2);

            Restaurant rest3 = new Restaurant();
            Restaurant rest4 = new Restaurant();
            rest3.Title = "TITLE3";
            rest4.Title = "TITLE4";
            restaurants.Add(rest3);
            restaurants.Add(rest4);
            return View(restaurants);
        }
        [HttpPost]
        public IActionResult SearchRestaurant(RestaurantSearch searchModel)
        {

            try
            {
                if (searchModel != null)
                {

                    string json = JsonSerializer.Serialize(searchModel);
                    WebRequest request = WebRequest.Create("https://localhost:7163/api/Restaurant/SearchRestaurants");

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();

                    WebResponse response = request.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    List<Restaurant> restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(restaurants);
                }
                else
                {
                    //no search
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchRestaurant (View): {ex.Message}\n{ex.StackTrace}");
            }

            return View();


        }



        [HttpPost]
        public IActionResult SearchRestaurantReviewer(RestaurantSearch searchModel)
        {

            try
            {
                if (searchModel != null)
                {

                    string json = JsonSerializer.Serialize(searchModel);
                    WebRequest request = WebRequest.Create("https://localhost:7163/api/Restaurant/SearchRestaurants");

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();

                    WebResponse response = request.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    List<Restaurant> restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(restaurants);

                }
                else
                {
                    List<Restaurant> restaurants = new List<Restaurant>();
                    return View(restaurants);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchRestaurant (View): {ex.Message}\n{ex.StackTrace}");
            }

            return View();


        }




        [HttpPost]
        public IActionResult SearchRestaurantRepresentative(RestaurantSearch searchModel)
        {

            try
            {
                if (searchModel != null)
                {

                    string json = JsonSerializer.Serialize(searchModel);
                    WebRequest request = WebRequest.Create("https://localhost:7163/api/Restaurant/SearchRestaurants");

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();

                    WebResponse response = request.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    List<Restaurant> restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(restaurants);
                }
                else
                {
                    //no search
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchRestaurant (View): {ex.Message}\n{ex.StackTrace}");
            }

            return View();


        }


        public IActionResult ManageRestaurants()
        {
            string id = HttpContext.Session.GetString("AccountID");
            int accountID = int.Parse(id);





            string url = "https://localhost:7163/api/Restaurant/GetRepresentativeRestaurants/" + accountID;
            WebRequest getRequest = WebRequest.Create(url);
            getRequest.Method = "GET";




            WebResponse response = getRequest.GetResponse();

            Stream theDataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(theDataStream, System.Text.Encoding.UTF8);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
            List<Restaurant> restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(restaurants);




        }

        [HttpPost]
        public IActionResult DeleteRestaurant(int id)
        {

            WebRequest request = WebRequest.Create("https://localhost:7163/api/Restaurant/DeleteRestaurant/" + id);

            request.Method = "DELETE";
            Console.WriteLine($"Attempting to delete restaurant with ID: {id}");

            try
            {
                using (WebResponse response = request.GetResponse())
                {

                    Console.WriteLine($"DELETE request successful. Status Code: {(response as HttpWebResponse)?.StatusCode}");
                    return RedirectToAction("ManageRestaurants");
                }
            }
            catch (WebException ex)
            {

                Console.WriteLine($"Error during DELETE request: {ex.Message}");
                return RedirectToAction("ManageRestaurants");

            }



        }



        [HttpGet]
        public IActionResult GetEditRestaurant(int id)
        {
            string url = "https://localhost:7163/api/Restaurant/ViewRestaurant/" + id;
            WebRequest getRequest = WebRequest.Create(url);
            getRequest.Method = "GET";




            WebResponse response = getRequest.GetResponse();

            Stream theDataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(theDataStream, System.Text.Encoding.UTF8);
            string data = reader.ReadToEnd();
            Console.WriteLine(data);
            Restaurant restaurant = JsonSerializer.Deserialize<Restaurant>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View("ModifyRestaurant", restaurant);
        }

        [HttpPut]
        public IActionResult EditRestaurant(int id)
        {
            return RedirectToAction("ManageRestaurants");
        }
    }
}
    
