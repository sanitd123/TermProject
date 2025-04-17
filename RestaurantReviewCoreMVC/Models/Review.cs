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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public int Quality
        {
            get { return quality; }
            set { quality = value; }
        }
        public int Service
        {
            get { return service; }
            set { service = value; }
        }
        public int Atmosphere
        {
            get { return atmosphere; }
            set { atmosphere = value; }
        }
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
