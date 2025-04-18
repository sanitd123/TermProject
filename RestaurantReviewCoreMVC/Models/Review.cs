using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewCoreMVC.Models
{
    public class Review
    {
        private int reviewID = -1;
        private int accountID = -1;
        private int restaurantID = -1;
        private string name = "";
        private string comment = "";
        private int quality = -1;
        private int service = -1;
        private int atmosphere = -1;
        private int price = -1;
        private DateTime visitTime = default;

        public int ReviewID
        {
            get { return reviewID; }
            set { reviewID = value; }
        }
        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
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
        [Required(ErrorMessage = "Comment required")]
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        [Range(1, 5, ErrorMessage = "Pick a quality rating")]
        public int Quality
        {
            get { return quality; }
            set { quality = value; }
        }
        [Range(1, 5, ErrorMessage = "Pick a service rating")]
        public int Service
        {
            get { return service; }
            set { service = value; }
        }
        [Range(1, 5, ErrorMessage = "Pick an atmosphere rating")]
        public int Atmosphere
        {
            get { return atmosphere; }
            set { atmosphere = value; }
        }
        [Range(1, 5, ErrorMessage = "Pick a price rating")]
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public DateTime VisitTime
        {
            get { return visitTime; }
            set { visitTime = value; }
        }
    }
}
