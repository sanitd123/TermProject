namespace RestaurantReviewCoreMVC.Models
{
    public class ResetPassword
    {

        private string code;
        private string newPassword;

        public string Code { get => code; set => code = value; }
        public string NewPassword { get => newPassword; set => newPassword = value; }
    }
}
