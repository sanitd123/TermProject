namespace RestaurantReviewCoreMVC.Models
{
    public class ForgotPassword
    {

        string email;
        string answerOne;
        string answerTwo;
        string answerThree;

        public string Email { get => email; set => email = value; }
        public string AnswerOne { get => answerOne; set => answerOne = value; }
        public string AnswerTwo { get => answerTwo; set => answerTwo = value; }
        public string AnswerThree { get => answerThree; set => answerThree = value; }
    }
}
