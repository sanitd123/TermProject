using Microsoft.AspNetCore.Mvc;
using RestaurantReviewCoreMVC.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SanithAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private DBConnect db = new DBConnect();
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/Review/GetAllReviewsByRestaurant/{id} - Select all reviews for a restaurant by restaurantID
        [HttpGet("GetAllReviewsByRestaurant/{id}")]
        public List<Review> GetAllReviewsByRestaurant(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetAllReviewsByRestaurant"
            };

            objCommand.Parameters.AddWithValue("@restaurantID", id);

            DataSet myData = db.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = myData.Tables[0];

            List<Review> reviewList = new List<Review>();
            for (int i = 0; i <  dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                Review review = new Review();
                review.ReviewID = int.Parse(row["REVIEW_ID"].ToString());
                review.AccountID = int.Parse(row["ACCOUNT_ID"].ToString());
                review.RestaurantID = int.Parse(row["RESTAURANT_ID"].ToString());
                review.Name = row["NAME"].ToString();
                review.Comment = row["COMMENT"].ToString();
                review.Quality = int.Parse(row["QUALITY"].ToString());
                review.Service = int.Parse(row["SERVICE"].ToString());
                review.Atmosphere = int.Parse(row["ATMOSPHERE"].ToString());
                review.Price = int.Parse(row["PRICE"].ToString());
                review.VisitTime = DateTime.Parse(row["VISIT_TIME"].ToString());
                review.RestaurantName = GetRestaurantNameByID(review.RestaurantID);

                reviewList.Add(review);
            }

            return reviewList;
        }

        // GET: api/Review/reviewer/{id} - Select all reviews from a reviewer by accountID(profileID)
        [HttpGet("GetAllReviewsByReviewer/{id}")]
        public List<Review> GetAllReviewsByReviewer(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetAllReviewsByReviewer"
            };

            objCommand.Parameters.AddWithValue("@accountID", id); // or profileID

            DataSet myData = db.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = myData.Tables[0];

            List<Review> reviewList = new List<Review>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                Review review = new Review();
                review.ReviewID = int.Parse(row["REVIEW_ID"].ToString());
                review.AccountID = int.Parse(row["ACCOUNT_ID"].ToString());
                review.RestaurantID = int.Parse(row["RESTAURANT_ID"].ToString());
                review.Name = row["NAME"].ToString();
                review.Comment = row["COMMENT"].ToString();
                review.Quality = int.Parse(row["QUALITY"].ToString());
                review.Service = int.Parse(row["SERVICE"].ToString());
                review.Atmosphere = int.Parse(row["ATMOSPHERE"].ToString());
                review.Price = int.Parse(row["PRICE"].ToString());
                review.VisitTime = DateTime.Parse(row["VISIT_TIME"].ToString());
                review.RestaurantName = GetRestaurantNameByID(review.RestaurantID);

                reviewList.Add(review);
            }

            return reviewList;
        }

        // GET: api/Review/GetReview/id - get review by reviewID
        [HttpGet("GetReview/{id}")]
        public Review GetReview(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetReview"
            };

            objCommand.Parameters.AddWithValue("@reviewID", id);

            DataSet myData = db.GetDataSetUsingCmdObj(objCommand);
            DataRow row = myData.Tables[0].Rows[0];

            Review review = new Review();
            review.ReviewID = int.Parse(row["REVIEW_ID"].ToString());
            review.AccountID = int.Parse(row["ACCOUNT_ID"].ToString());
            review.RestaurantID = int.Parse(row["RESTAURANT_ID"].ToString());
            review.Name = row["NAME"].ToString();
            review.Comment = row["COMMENT"].ToString();
            review.Quality = int.Parse(row["QUALITY"].ToString());
            review.Service = int.Parse(row["SERVICE"].ToString());
            review.Atmosphere = int.Parse(row["ATMOSPHERE"].ToString());
            review.Price = int.Parse(row["PRICE"].ToString());
            review.VisitTime = DateTime.Parse(row["VISIT_TIME"].ToString());
            review.RestaurantName = GetRestaurantNameByID(review.RestaurantID);

            return review;
        }

        // POST: api/Review/InsertReview - insert review
        [HttpPost("InsertReview")]        
        public void Insert([FromBody] Review review)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_InsertReview"
            };

            objCommand.Parameters.AddWithValue("@accountID", review.AccountID);
            objCommand.Parameters.AddWithValue("@restaurantID", review.RestaurantID);
            objCommand.Parameters.AddWithValue("@name", review.Name);
            objCommand.Parameters.AddWithValue("@comment", review.Comment);
            objCommand.Parameters.AddWithValue("@quality", review.Quality);
            objCommand.Parameters.AddWithValue("@service", review.Service);
            objCommand.Parameters.AddWithValue("@atmosphere", review.Atmosphere);
            objCommand.Parameters.AddWithValue("@price", review.Price);
            objCommand.Parameters.AddWithValue("@visitTime", review.VisitTime);

            db.DoUpdateUsingCmdObj(objCommand);

            UpdateAverageRating(review.RestaurantID);
        }

        // PUT: api/Review/UpdateReview - Update review with reviewID
        [HttpPut("UpdateReview")]
        public void Update([FromBody] Review review)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_UpdateReview"
            };

            objCommand.Parameters.AddWithValue("@reviewID", review.ReviewID);
            objCommand.Parameters.AddWithValue("@name", review.Name);
            objCommand.Parameters.AddWithValue("@comment", review.Comment);
            objCommand.Parameters.AddWithValue("@quality", review.Quality);
            objCommand.Parameters.AddWithValue("@service", review.Service);
            objCommand.Parameters.AddWithValue("@atmosphere", review.Atmosphere);
            objCommand.Parameters.AddWithValue("@price", review.Price);
            objCommand.Parameters.AddWithValue("@visitTime", review.VisitTime);

            db.DoUpdateUsingCmdObj(objCommand);

            UpdateAverageRating(review.RestaurantID);
        }

        // DELETE: api/Review/DeleteReview/{id} - Delete review with reviewID
        [HttpDelete("DeleteReview/{id}")]
        public void Delete(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_DeleteReview"
            };

            objCommand.Parameters.AddWithValue("@reviewID", id);

            db.DoUpdateUsingCmdObj(objCommand);

            int restaurantID = GetRestaurantIDByReviewID(id);
            UpdateAverageRating(restaurantID);
        }

        private void UpdateAverageRating(int restaurantID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_UpdateAverageRating"
            };

            objCommand.Parameters.AddWithValue("@restaurantID", restaurantID);

            db.DoUpdateUsingCmdObj(objCommand);
        }

        private int GetRestaurantIDByReviewID(int reviewID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetRestaurantIDByReviewID"
            };

            objCommand.Parameters.AddWithValue("@reviewID", reviewID);

            SqlParameter restaurantIDParam = new SqlParameter
            {
                ParameterName = "@restaurantID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            objCommand.Parameters.Add(restaurantIDParam);

            db.GetDataSetUsingCmdObj(objCommand);

            int restaurantID = Convert.ToInt32(objCommand.Parameters["@restaurantID"].Value);
            return restaurantID;
        }
        private string GetRestaurantNameByID(int restaurantID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetRestaurantNameByID"
            };

            objCommand.Parameters.AddWithValue("@restaurantID", restaurantID);

            SqlParameter restaurantNameParam = new SqlParameter
            {
                ParameterName = "@restaurantName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            };

            objCommand.Parameters.Add(restaurantNameParam);

            db.GetDataSetUsingCmdObj(objCommand);

            string restaurantName = objCommand.Parameters["@restaurantName"].Value.ToString();
            return restaurantName;
        }
    }
}
