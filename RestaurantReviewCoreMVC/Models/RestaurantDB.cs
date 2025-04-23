using Microsoft.Data.SqlClient;
using System.Data;

namespace RestaurantReviewCoreMVC.Models
{
    public class RestaurantDB
    {
        private DBConnect db = new DBConnect();

        public List<Reservation> GetAllReservationsByRestaurant(int restaurantID)
        {
            SqlCommand objCommand = new SqlCommand 
            { 
                CommandType = CommandType.StoredProcedure, 
                CommandText = "TP_GetAllReservationsByRestaurant" 
            };

            objCommand.Parameters.AddWithValue("@restaurantID", restaurantID);

            DataSet myData = db.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = myData.Tables[0];

            List<Reservation> reservationList = new List<Reservation>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Reservation reservation = new Reservation();
                reservation.ReservationID = int.Parse(dt.Rows[i]["RESERVATION_ID"].ToString());
                reservation.RestaurantID = int.Parse(dt.Rows[i]["RESTAURANT_ID"].ToString());
                reservation.Name = dt.Rows[i]["NAME"].ToString();
                reservation.Phone = dt.Rows[i]["PHONE"].ToString();
                reservation.Email = dt.Rows[i]["EMAIL"].ToString();
                reservation.ReservationTime = DateTime.Parse(dt.Rows[i]["RESERVATION_TIME"].ToString());
                reservation.PartySize = int.Parse(dt.Rows[i]["PARTY_SIZE"].ToString());
                reservation.Comment = dt.Rows[i]["COMMENT"].ToString();
                reservation.Status = dt.Rows[i]["STATUS"].ToString();
                reservation.RestaurantName = GetRestaurantNameByID(reservation.RestaurantID);

                reservationList.Add(reservation);
            }

            return reservationList;
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
            DataRow row = myData.Tables[0].Rows[0];

            Reservation reservation = new Reservation();
            reservation.ReservationID = int.Parse(row["RESERVATION_ID"].ToString());
            reservation.RestaurantID = int.Parse(row["RESTAURANT_ID"].ToString());
            reservation.Name = row["NAME"].ToString();
            reservation.Phone = row["PHONE"].ToString();
            reservation.Email = row["EMAIL"].ToString();
            reservation.ReservationTime = DateTime.Parse(row["RESERVATION_TIME"].ToString());
            reservation.PartySize = int.Parse(row["PARTY_SIZE"].ToString());
            reservation.Comment = row["COMMENT"].ToString();
            reservation.Status = row["STATUS"].ToString();
            reservation.RestaurantName = GetRestaurantNameByID(reservation.RestaurantID);

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

            string comment = reservation.Comment;
            if (comment.Equals(string.Empty))
            {
                comment = "NONE";
            }

            objCommand.Parameters.AddWithValue("@comment", comment);

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
            objCommand.Parameters.AddWithValue("@status", "Modified");

            string comment = reservation.Comment;
            if (comment.Equals(string.Empty))
            {
                comment = "NONE";
            }

            objCommand.Parameters.AddWithValue("@comment", comment);

            db.DoUpdateUsingCmdObj(objCommand);
        }
        public void AcceptReservation(int reservationID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_AcceptReservation"
            };

            objCommand.Parameters.AddWithValue("@reservationID", reservationID);

            db.DoUpdateUsingCmdObj(objCommand);
        }
        public void DeclineReservation(int reservationID) // update
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_DeclineReservation"
            };

            objCommand.Parameters.AddWithValue("@reservationID", reservationID);

            db.DoUpdateUsingCmdObj(objCommand);
        }
        public string GetRestaurantNameByID(int restaurantID)
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
        public string GetEmailByAccount(int accountID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetEmailByAccount"
            };

            objCommand.Parameters.AddWithValue("@accountID", accountID);

            SqlParameter emailParam = new SqlParameter
            {
                ParameterName = "@email",
                SqlDbType = SqlDbType.NVarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            };

            objCommand.Parameters.Add(emailParam);

            db.GetDataSetUsingCmdObj(objCommand);
            string email = objCommand.Parameters["@email"].Value.ToString();
            return email;
        }
        public int GetAccountIDByRestaurant(int restaurantID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetAccountIDByRestaurant"
            };

            objCommand.Parameters.AddWithValue("@restaurantID", restaurantID);

            SqlParameter accountIDParam = new SqlParameter
            {
                ParameterName = "@accountID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            objCommand.Parameters.Add(accountIDParam);

            db.GetDataSetUsingCmdObj(objCommand);
            int accountID = Convert.ToInt32(objCommand.Parameters["@accountID"].Value);
            return accountID;
        }
        public string GetEmailByReservation(int reservationID)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_GetEmailByReservation"
            };

            objCommand.Parameters.AddWithValue("@reservationID", reservationID);

            SqlParameter emailParam = new SqlParameter
            {
                ParameterName = "@email",
                SqlDbType = SqlDbType.NVarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            };

            objCommand.Parameters.Add(emailParam);

            db.GetDataSetUsingCmdObj(objCommand);
            string email = objCommand.Parameters["@email"].Value.ToString();
            return email;
        }
        public void UpdateExpiredReservations()
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "TP_UpdateExpiredReservations"
            };

            db.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
