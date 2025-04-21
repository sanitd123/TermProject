using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using RestaurantReviewCoreMVC.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace WertmanAPI.Controllers
{

    [ApiController]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {
        DBConnect objDB = new DBConnect();


        [HttpPost("AddRestaurant")]
        public Boolean AddRestaurantToDB([FromBody] Restaurant restaurant)
        {
            try
            {


                SqlCommand objCommand = new SqlCommand();


                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_GetAllRestaurants";
                DataSet myDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
                int newRestaurantID = 1;
                foreach (DataRow row in myDataSet.Tables[0].Rows)
                {
                    newRestaurantID++;
                }



                objCommand.CommandText = "TP_AddRestaurant";
                objCommand.Parameters.AddWithValue("@restaurantID", newRestaurantID);//
                objCommand.Parameters.AddWithValue("@cuisine", restaurant.Cuisine);

                objCommand.Parameters.AddWithValue("@hours", restaurant.Hours);
                objCommand.Parameters.AddWithValue("@restaurantName", restaurant.Title);//

                objCommand.Parameters.AddWithValue("@street", restaurant.Street);
                objCommand.Parameters.AddWithValue("@city", restaurant.City);
                objCommand.Parameters.AddWithValue("@state", restaurant.State);
                objCommand.Parameters.AddWithValue("@zip", restaurant.Zipcode);
                objCommand.Parameters.AddWithValue("@phone", restaurant.Phone);
                objCommand.Parameters.AddWithValue("@email", restaurant.Email);
                objCommand.Parameters.AddWithValue("@averageScore", 0.0);//

                //string accID = HttpContext.Session.GetString("AccountID");

                objCommand.Parameters.AddWithValue("@marketingDesc", restaurant.Description);//
                objCommand.Parameters.AddWithValue("@siteURL", restaurant.Website);
                objCommand.Parameters.AddWithValue("@accountID", restaurant.AccountID); //testing//
                objCommand.Parameters.AddWithValue("@owner", restaurant.Owner);


                objDB.DoUpdateUsingCmdObj(objCommand);
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in AddRestaurantToDB: {e.Message}\n{e.StackTrace}");
                return false;
            }


        }


        [HttpPut("UpdateRestaurant/{restaurant.RestaurantID}")]
        public Boolean UpdateRestaurant([FromBody] Restaurant restaurant)
        {
            try
            {
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_UpdateRestaurant";
                objCommand.Parameters.AddWithValue("@restaurantID", restaurant.RestaurantID);
                objCommand.Parameters.AddWithValue("@cuisine", restaurant.Cuisine);

                objCommand.Parameters.AddWithValue("@hours", restaurant.Hours);
                objCommand.Parameters.AddWithValue("@restaurantName", restaurant.Title);

                objCommand.Parameters.AddWithValue("@street", restaurant.Street);
                objCommand.Parameters.AddWithValue("city", restaurant.City);
                objCommand.Parameters.AddWithValue("@state", restaurant.State);
                objCommand.Parameters.AddWithValue("@zip", restaurant.Zipcode);
                objCommand.Parameters.AddWithValue("@phone", restaurant.Phone);
                objCommand.Parameters.AddWithValue("@email", restaurant.Email);
                objCommand.Parameters.AddWithValue("@averageScore", restaurant.AvgScore);


                objCommand.Parameters.AddWithValue("@marketingDesc", restaurant.Description);
                objCommand.Parameters.AddWithValue("@siteURL", restaurant.Website);
                objCommand.Parameters.AddWithValue("@accountID", restaurant.AccountID);
                objCommand.Parameters.AddWithValue("@owner", restaurant.Owner);


                objDB.DoUpdateUsingCmdObj(objCommand);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;


        }


        [HttpGet("ViewRestaurant/{restaurantID}")]
        public Restaurant GetRestaurant(int restaurantID)
        {

            SqlCommand objCommand = new SqlCommand();
            Restaurant restaurantToView = new Restaurant();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetRestaurantByID";
            objCommand.Parameters.AddWithValue("@restaurantID", restaurantID);



            DataSet myDataSet = objDB.GetDataSet(objCommand);
            foreach (DataRow row in myDataSet.Tables[0].Rows)
            {
                restaurantToView.RestaurantID = restaurantID;
                restaurantToView.Title = row["RestaurantName"].ToString();
                restaurantToView.Cuisine = row["Cuisine"].ToString();

                restaurantToView.Hours = row["Hours"].ToString();

                restaurantToView.Street = row["Street"].ToString();
                restaurantToView.City = row["City"].ToString();
                restaurantToView.State = row["State"].ToString();
                restaurantToView.Zipcode = int.Parse(row["Zip"].ToString());
                restaurantToView.Phone = row["Phone"].ToString();
                restaurantToView.Email = row["Email"].ToString();
                restaurantToView.AvgScore = float.Parse(row["AverageScore"].ToString());


                restaurantToView.Description = row["MarketingDesc"].ToString();
                restaurantToView.Website = row["SiteURL"].ToString();
                restaurantToView.AccountID = int.Parse(row["AccountID"].ToString());
                restaurantToView.Owner = row["Owner"].ToString();

                restaurantToView.GalleryPhotoList = null;
                List<Review> reviews = new List<Review>();
                Review review = new Review();
                review.Name = "Test";
                review.Comment = "This is me testing display for the reviews";
                review.VisitTime= DateTime.Now;
                review.Atmosphere = 4;
                review.Service = 3;
                review.Price = 3;
                review.Quality = 5;

                reviews.Add(review);
                restaurantToView.ReviewList = reviews;
            }
            return restaurantToView;






        }



        [HttpPost("SearchRestaurants")]
        public List<Restaurant> SearchRestaurants([FromBody] RestaurantSearch criteria)
        {

            List<Restaurant> restaurants = new List<Restaurant>();
            DataSet oneRestaurant = new DataSet();
            DataSet restaurantsFull = new DataSet();

            foreach (string cuisine in criteria.Cuisines)
            {
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_GetRestaurantsByCriteria";
                objCommand.Parameters.AddWithValue("@city", criteria.City);
                objCommand.Parameters.AddWithValue("@cuisine", cuisine);
                oneRestaurant = objDB.GetDataSet(objCommand);
                restaurantsFull.Merge(oneRestaurant);





            }

            foreach (DataRow row in restaurantsFull.Tables[0].Rows)
            {
                Restaurant result = new Restaurant();

                result.RestaurantID = int.Parse(row["RestaurantID"].ToString());
                result.Title = row["RestaurantName"].ToString();
                result.Cuisine = row["Cuisine"].ToString();

                result.Hours = row["Hours"].ToString();

                result.Street = row["Street"].ToString();
                result.City = row["City"].ToString();
                result.State = row["State"].ToString();
                result.Zipcode = int.Parse(row["Zip"].ToString());
                result.Phone = row["Phone"].ToString();
                result.Email = row["Email"].ToString();
                result.AvgScore = float.Parse(row["AverageScore"].ToString());


                result.Description = row["MarketingDesc"].ToString();
                result.Website = row["SiteURL"].ToString();
                result.AccountID = int.Parse(row["AccountID"].ToString());
                result.Owner = row["Owner"].ToString();

                restaurants.Add(result);
            }


            return restaurants;
        }




        [HttpGet("GetRepresentativeRestaurants/{accountID}")]
        public List<Restaurant> GetRepresentativeRestaurants(int accountID)
        {

            List<Restaurant> restaurants = new List<Restaurant>();


            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetRestaurantsByAccountID";
            objCommand.Parameters.AddWithValue("@accountID", accountID);
            DataSet restaurantsFull = objDB.GetDataSet(objCommand);

            foreach (DataRow row in restaurantsFull.Tables[0].Rows)
            {
                Restaurant result = new Restaurant();

                result.RestaurantID = int.Parse(row["RestaurantID"].ToString());
                result.Title = row["RestaurantName"].ToString();
                result.Cuisine = row["Cuisine"].ToString();

                result.Hours = row["Hours"].ToString();

                result.Street = row["Street"].ToString();
                result.City = row["City"].ToString();
                result.State = row["State"].ToString();
                result.Zipcode = int.Parse(row["Zip"].ToString());
                result.Phone = row["Phone"].ToString();
                result.Email = row["Email"].ToString();
                result.AvgScore = float.Parse(row["AverageScore"].ToString());


                result.Description = row["MarketingDesc"].ToString();
                result.Website = row["SiteURL"].ToString();
                result.AccountID = int.Parse(row["AccountID"].ToString());
                result.Owner = row["Owner"].ToString();

                restaurants.Add(result);
            }

            return restaurants;
        }


        [HttpDelete("DeleteRestaurant/{restaurantID}")]
        public void DeleteRestaurant(int restaurantID)
        {
            SqlCommand objCommand = new SqlCommand();
            
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_DeleteRestaurant";
            objCommand.Parameters.AddWithValue("@restaurantID", restaurantID);

            objDB.DoUpdate(objCommand);
        }
    }
}
