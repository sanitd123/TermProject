using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using RestaurantReviewCoreMVC.Models;
using System.Data;

namespace WertmanAPI.Controllers
{

    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        DBConnect objDB = new DBConnect();
        private IPasswordHasher<Account> passwordHasher;

        public AccountController()
        {
            passwordHasher = new PasswordHasher<Account>();
        }


        [HttpPost("Login")]
        public String Login([FromBody] Account account)
        {

            try
            {
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_GetAllAccounts";

                DataSet myDataSet = objDB.GetDataSet(objCommand);

                foreach (DataRow row in myDataSet.Tables[0].Rows)
                {

                    
                    if (row["AccountEmail"].Equals(account.Email) && (passwordHasher.VerifyHashedPassword(account, row["AccountPassword"].ToString(), account.Password) == PasswordVerificationResult.Success))
                    {
                        return "true";


                    }

                }
                return "false";
            }
            catch (Exception ex)
            {
                return "false";
            }

        }
        [HttpGet("GetAccount/{accountEmail}")]
        public Account GetAccount(string accountEmail)
        {
            try
            {
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_GetAccountByEmail";
                objCommand.Parameters.AddWithValue("@accountEmail", accountEmail);

                DataSet account = objDB.GetDataSet(objCommand);
                Account returnAccount = new Account();
                foreach (DataRow row in account.Tables[0].Rows)
                {
                    returnAccount.AccountID = int.Parse(row["AccountID"].ToString());
                    returnAccount.Email = row["AccountEmail"].ToString();
                    returnAccount.Name = row["AccountOwner"].ToString();
                    returnAccount.AccountType = row["AccountType"].ToString();
                    returnAccount.Password = "";
                    returnAccount.AnswerOne = row["AnswerOne"].ToString();
                    returnAccount.AnswerTwo = row["AnswerTwo"].ToString();
                    returnAccount.AnswerThree = row["AnswerThree"].ToString();
                }

                return returnAccount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting account: {ex.Message}\n{ex.StackTrace}");
                return null;

            }
        }

        [HttpPost("CreateAccount")]
        public Boolean AddAccount([FromBody] Account account)
        {
            
            
            try
            {
                account.Password = passwordHasher.HashPassword(account, account.Password);
                SqlCommand objCommand = new SqlCommand();


                objCommand.CommandType= CommandType.StoredProcedure;
                
                
                objCommand.CommandText = "TP_AddAccount";
                objCommand.Parameters.AddWithValue("@accountOwner", account.Name);
                objCommand.Parameters.AddWithValue("@accountPassword", account.Password);
                objCommand.Parameters.AddWithValue("@accountEmail", account.Email);
                objCommand.Parameters.AddWithValue("@accountType", account.AccountType);
                objCommand.Parameters.AddWithValue("@answerOne", account.AnswerOne);
                objCommand.Parameters.AddWithValue("@answerTwo", account.AnswerTwo);
                objCommand.Parameters.AddWithValue("@answerThree", account.AnswerThree);

                objDB.DoUpdateUsingCmdObj(objCommand);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding account: {ex.Message}\n{ex.StackTrace}");
                return false;
            }

        }
    }
}
