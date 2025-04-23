using System.Net.Mail;
using System.Security.Cryptography;

namespace RestaurantReviewCoreMVC.Models
{
    public class ResetPassword
    {

        private string code;
        private string newPassword;

        public string Code { get => code; set => code = value; }
        public string NewPassword { get => newPassword; set => newPassword = value; }


        public void GenerateCode()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            byte[] randomBytes = new byte[6];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            char[] chars = new char[6];
            for (int i = 0; i < 6; i++)
            {
                chars[i] = allowedChars[randomBytes[i] % allowedChars.Length];
            }

            NewPassword = new string(chars);
        }



        public void SendEmailCode()
        {
            SmtpClient smtpClient = new SmtpClient();
            
        }
    }
}
