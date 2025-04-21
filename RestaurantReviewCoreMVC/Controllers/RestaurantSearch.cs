namespace RestaurantReviewCoreMVC.Models
{
    public class RestaurantSearch
    {
        private string city;
        private List<string> cuisines;

        public string City { get => city; set => city = value; }
        public List<string> Cuisines { get => cuisines; set => cuisines = value; }
    }
}