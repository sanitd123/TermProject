using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewCoreMVC.Models
{
    public class Reservation
    {
        private int reservationID = -1;
        private int restaurantID = -1;

        private string name = "";
        private string phone = "";
        private string email = "";

        private DateTime reservationTime = default;
        private int partySize = -1;
        private string comment = "";
        private string status = "";

        public int ReservationID
        {
            get { return reservationID; }
            set { reservationID = value; }
        }
        public int RestaurantID
        {
            get { return restaurantID; }
            set { restaurantID = value; }
        }
        [Required(ErrorMessage = "Name required")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [Required(ErrorMessage = "Phone number required")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        [Required(ErrorMessage = "Email required")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public DateTime ReservationTime
        {
            get { return reservationTime; }
            set { reservationTime = value; }
        }
        
        public int PartySize
        {
            get { return partySize; }
            set { partySize = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
