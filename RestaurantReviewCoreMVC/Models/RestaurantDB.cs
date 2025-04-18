using Microsoft.Data.SqlClient;
using System.Data;

namespace RestaurantReviewCoreMVC.Models
{
    public class RestaurantDB
    {
        private DBConnect db = new DBConnect();
        public Review GetReview(int? reviewID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetReview"
            };

            objCommand.Parameters.AddWithValue("@reviewID", reviewID);

            DataSet myData = db.GetDataSetUsingCmdObj(objCommand);

            if (myData.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            Review review = new Review();
            review.ReviewID = int.Parse(myData.Tables[0].Rows[0]["REVIEW_ID"].ToString());
            review.AccountID = int.Parse(myData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString());
            review.RestaurantID = int.Parse(myData.Tables[0].Rows[0]["RESTAURANT_ID"].ToString());
            review.Name = myData.Tables[0].Rows[0]["NAME"].ToString();
            review.Comment = myData.Tables[0].Rows[0]["COMMENT"].ToString();
            review.Quality = int.Parse(myData.Tables[0].Rows[0]["QUALITY"].ToString());
            review.Service = int.Parse(myData.Tables[0].Rows[0]["SERVICE"].ToString());
            review.Atmosphere = int.Parse(myData.Tables[0].Rows[0]["ATMOSPHERE"].ToString());
            review.Price = int.Parse(myData.Tables[0].Rows[0]["PRICE"].ToString());
            review.VisitTime = DateTime.Parse(myData.Tables[0].Rows[0]["VISIT_TIME"].ToString());

            return review;
        }
        public void InsertReview(Review review)
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
        }
        public void UpdateReview(Review review)
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
        }
        public Reservation GetReservation(int? reservationID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetReservation"
            };

            objCommand.Parameters.AddWithValue("@reservationID", reservationID);

            DataSet myData = db.GetDataSetUsingCmdObj(objCommand);

            Reservation reservation = new Reservation();
            reservation.ReservationID = int.Parse(myData.Tables[0].Rows[0]["RESERVATION_ID"].ToString());
            reservation.RestaurantID = int.Parse(myData.Tables[0].Rows[0]["RESTAURANT_ID"].ToString());
            reservation.Name = myData.Tables[0].Rows[0]["NAME"].ToString();
            reservation.Phone = myData.Tables[0].Rows[0]["PHONE"].ToString();
            reservation.Email = myData.Tables[0].Rows[0]["EMAIL"].ToString();
            reservation.ReservationTime = DateTime.Parse(myData.Tables[0].Rows[0]["RESERVATION_TIME"].ToString());
            reservation.PartySize = int.Parse(myData.Tables[0].Rows[0]["PARTY_SIZE"].ToString());
            reservation.Comment = myData.Tables[0].Rows[0]["COMMENT"].ToString();
            reservation.Status = myData.Tables[0].Rows[0]["STATUS"].ToString();

            return reservation;
        }
        public void InsertReservation(Reservation reservation)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_InsertReservation"
            };

            objCommand.Parameters.AddWithValue("@restaurantID", reservation.RestaurantID);
            objCommand.Parameters.AddWithValue("@name", reservation.Name);
            objCommand.Parameters.AddWithValue("@phone", reservation.Phone);
            objCommand.Parameters.AddWithValue("@email", reservation.Email);
            objCommand.Parameters.AddWithValue("@reservationTime", reservation.ReservationTime);
            objCommand.Parameters.AddWithValue("@partySize", reservation.PartySize);
            objCommand.Parameters.AddWithValue("@comment", reservation.Comment);

            db.DoUpdateUsingCmdObj(objCommand);
        }

        public void UpdateReservation(Reservation reservation)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_UpdateReservation"
            };

            objCommand.Parameters.AddWithValue("@reservationID", reservation.ReservationID);
            objCommand.Parameters.AddWithValue("@name", reservation.Name);
            objCommand.Parameters.AddWithValue("@phone", reservation.Phone);
            objCommand.Parameters.AddWithValue("@email", reservation.Email);
            objCommand.Parameters.AddWithValue("@reservationTime", reservation.ReservationTime);
            objCommand.Parameters.AddWithValue("@partySize", reservation.PartySize);
            objCommand.Parameters.AddWithValue("@comment", reservation.Comment);
            objCommand.Parameters.AddWithValue("@status", reservation.Status);

            db.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
