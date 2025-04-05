namespace RestaurantReviewCoreMVC.Models
{
    public class AccountModel
    {
        private int accountID;
        private string name;
        private string email;
        private string password;
        private string accountType;

        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }
    }
}
