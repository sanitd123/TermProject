using Microsoft.AspNetCore.Mvc.ViewEngines;
using RestaurantReviewCoreMVC.ThirdPartyApi;

namespace RestaurantReviewCoreMVC.Models
{
    public class Restaurant
    {
        private int restaurantID;
        private int accountID;

        private string title;
        private string description;
        private double avgScore;
        private string cuisine;

        private string street;
        private string city;
        private string state;
        private int zipcode;

        private string phone;
        private string email;
        private string website;
        private string hours;
        private string owner;

        private List<GalleryPhoto> galleryPhotoList = new List<GalleryPhoto>();
        private List<Review> reviewList = new List<Review>();

        private Coordinate coordinate = new Coordinate();

        public void FindCoordinate()
        {
            string fullAddress = street + ", " + city + ", " + state;
            NominatimApi api = new NominatimApi();
            coordinate = api.GetCoordinate(fullAddress);
        }
        public int RestaurantID
        {
            get { return restaurantID; }
            set { restaurantID = value; }
        }
        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public double AvgScore
        {
            get { return avgScore; }
            set { avgScore = value; }
        }
        public string Cuisine
        {
            get { return cuisine; }
            set { cuisine = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        public int Zipcode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        public string Hours { get => hours; set => hours = value; }
        public string Owner { get => owner; set => owner = value; }
        public List<GalleryPhoto> GalleryPhotoList { get => galleryPhotoList; set => galleryPhotoList = value; }
        public List<Review> ReviewList { get => reviewList; set => reviewList = value; }

        public Coordinate Coordinate
        {
            get { return coordinate; }
            set { coordinate = value; }
        }
    }
}
