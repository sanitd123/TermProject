namespace RestaurantReviewCoreMVC.Models
{
    public class GalleryPhoto
    {
        private int imageID;
        private int restaurantID;
        private string title;
        private string caption;
        private string imageUrl;

        public int ImageID
        {
            get { return imageID; }
            set { imageID = value; }
        }
        public int RestaurantID
        {
            get { return restaurantID; }
            set { restaurantID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
    }
}
