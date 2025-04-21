using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewCoreMVC.Models;
using System.Net;
using System.Text.Json;

namespace RestaurantReviewCoreMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ResetPassword()
        {
            //get acc
            return View();
        }
        
        [HttpGet] 
        public IActionResult CreateAccount()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {

            if (account != null)
            {
                
                try
                {
                    
                    string json = JsonSerializer.Serialize(account);
                    WebRequest request = WebRequest.Create("https://localhost:7163/api/Account/CreateAccount");

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;

                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();

                    WebResponse response = request.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    String data = reader.ReadToEnd();

                    if (data == "true")
                    {

                        //go to landing page, account created
                        return View();
                    }
                    else if (data == "false")
                    {
                        //error, stay on account creation
                        return View();
                    }

                }
                catch (Exception ex)
                {
                    //error
                    return View();
                }


                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account account)



        {
            Console.WriteLine("In");
            if (account != null)
            {
                Console.WriteLine("Inside");
                try
                {
                    
                    string url = "https://localhost:7163/api/Account/GetAccount/" + account.Email;
                    WebRequest getRequest = WebRequest.Create(url);
                    getRequest.Method = "GET";
                    

                   

                    WebResponse response = getRequest.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    string data = reader.ReadToEnd();
                    Console.WriteLine(data);
                    Account retrievedAccount = JsonSerializer.Deserialize<Account>(data , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    retrievedAccount.Password = account.Password;
                    
                    Console.WriteLine($"Retrieved Account - AccountID: {retrievedAccount?.AccountID}, Email: {retrievedAccount?.Email}, Name: {retrievedAccount?.Name}, Password: {retrievedAccount?.Password}, ..."); 

                    


                    string json = JsonSerializer.Serialize(retrievedAccount);
                    
                    WebRequest request = WebRequest.Create("https://localhost:7163/api/Account/Login");

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;

                    Console.WriteLine(json);
                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();

                    response = request.GetResponse();
                    theDataStream = response.GetResponseStream();
                    reader = new StreamReader(theDataStream);
                    data = reader.ReadToEnd();
                    Console.WriteLine("Insidemore");
                    if (data == "true")
                    {
                        HttpContext.Session.SetString("AccountID", retrievedAccount.AccountID.ToString());
                        HttpContext.Session.SetString("AccountType", retrievedAccount.AccountType.ToString());
                        
                        
                        List<Restaurant> list = new List<Restaurant>();
                        return View("~/Views/Restaurant/SearchRestaurant.cshtml", list );

                            
                    }
                    else if (data == "false")
                    {
                        Console.WriteLine("It is false");
                        //error, stay on login
                        return View();
                    }



                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error login: {ex.Message}\n{ex.StackTrace}");
                    //issues
                }
            }
            return View();
        }



        public IActionResult Logout()
        {
            List<Restaurant> list = new List<Restaurant>();
            HttpContext.Session.Clear();
            return View("~/Views/Restaurant/SearchRestaurant.cshtml", list);
        }
    }
}
