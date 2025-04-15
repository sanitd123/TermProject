﻿using Microsoft.AspNetCore.Mvc;
using SanithAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace SanithAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/Review/restaurant/{id} - Select all reviews for a restaurant by restaurantID
        [HttpGet("restaurant/{id}")]
        public List<Review> GetAllReviewsByRestaurant(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetAllReviewsByRestaurant"
            };

            objCommand.Parameters.AddWithValue("@restaurantID", id);

            DBConnect db = new DBConnect();
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
                review.VisitTime = DateTime.Parse(row["VISIT_DATE"].ToString());

                reviewList.Add(review);
            }

            return reviewList;
        }

        // GET: api/Review/reviewer/{id} - Select all reviews from a reviewer by accountID(profileID)
        [HttpGet("reviewer/{id}")]
        public List<Review> GetAllReviewsByReviewer(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetAllReviewsByReviewer"
            };

            objCommand.Parameters.AddWithValue("@accountID", id); // or profileID

            DBConnect db = new DBConnect();
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
                review.VisitTime = DateTime.Parse(row["VISIT_DATE"].ToString());

                reviewList.Add(review);
            }

            return reviewList;
        }

        // TEST GET: api/Review
        [HttpGet]
        public string Get()
        {
            return "I can test my review api";
        }

        // GET: api/Review/{id} - Select review by reviewID
        [HttpGet("{id}")]
        public Review Get(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetReview"
            };

            objCommand.Parameters.AddWithValue("@reviewID", id);

            DBConnect db = new DBConnect();
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
            review.VisitTime = DateTime.Parse(row["VISIT_DATE"].ToString());

            return review;
        }

        // POST: api/Review - insert review
        [HttpPost]        
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

            DBConnect db = new DBConnect();
            db.DoUpdateUsingCmdObj(objCommand);
        }

        // PUT: api/Review - Update review with reviewID
        [HttpPut]
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

            DBConnect db = new DBConnect();
            db.DoUpdateUsingCmdObj(objCommand);
        }

        // DELETE: api/Review/{id} - Delete review with reviewID
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_DeleteReview"
            };

            objCommand.Parameters.AddWithValue("@reviewID", id);

            DBConnect db = new DBConnect();
            db.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
