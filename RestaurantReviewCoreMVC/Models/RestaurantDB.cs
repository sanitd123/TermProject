using Microsoft.Data.SqlClient;
using System.Data;

namespace RestaurantReviewCoreMVC.Models
{
    public class RestaurantDB
    {
        private DBConnect db = new DBConnect();
        public void InsertReview(Review review)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "InsertReview"
            };

            objCommand.Parameters.AddWithValue("@accountID", review.AccountID);
            objCommand.Parameters.AddWithValue("@restaurantID", review.RestaurantID);
            objCommand.Parameters.AddWithValue("@name", review.Name);
            objCommand.Parameters.AddWithValue("@comment", review.Comment);
            objCommand.Parameters.AddWithValue("@quality", review.Quality);
            objCommand.Parameters.AddWithValue("@service", review.Service);
            objCommand.Parameters.AddWithValue("@atmosphere", review.Atmosphere);
            objCommand.Parameters.AddWithValue("@price", review.Price);
            objCommand.Parameters.AddWithValue("@visitDate", review.VisitTime);

            db.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
