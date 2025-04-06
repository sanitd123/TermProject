namespace RestaurantReviewCoreMVC.Models
{
    public class Review
    {
        private int reviewID;
        private int accountID;
        private int restaurantID;
        private string name;
        private string comment;
        private int quality;
        private int service;
        private int atmosphere;
        private int price;
        private DateTime visitTime;

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
