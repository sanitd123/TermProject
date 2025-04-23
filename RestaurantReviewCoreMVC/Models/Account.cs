namespace RestaurantReviewCoreMVC.Models
{
    public class Account
    {
        private int accountID;
        private string name;
        private string email;
        private string password;
        private string accountType;
        private string answerOne;
        private string answerTwo;
        private string answerThree;


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

        public string AnswerOne { get => answerOne; set => answerOne = value; }
        public string AnswerTwo { get => answerTwo; set => answerTwo = value; }
        public string AnswerThree { get => answerThree; set => answerThree = value; }
    }
}
