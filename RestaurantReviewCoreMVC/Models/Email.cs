using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace RestaurantReviewCoreMVC.Models
{
    public class Email
    {
        private MailMessage objMail = new MailMessage();
        private MailAddress toAddress;
        private MailAddress fromAddress = new MailAddress("tui96569@temple.edu"); //always from my email
        private MailAddress ccAddress;
        private MailAddress bccAddress;
        private String subject;
        private String messageBody;
        private Boolean isHTMLBody = true;
        private MailPriority priority = MailPriority.Normal;
        private String mailHost = "smtp.temple.edu";

        public bool SendReservationRequest(Reservation reservation)
        {
            try
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                int accountID = restaurantDB.GetAccountIDByRestaurant(reservation.RestaurantID);
                string restaurantName = restaurantDB.GetRestaurantNameByID(reservation.RestaurantID);

                this.Recipient = restaurantDB.GetEmailByAccount(accountID);
                this.Subject = "New Reservation for " + reservation.Name;
                this.Message = "Greetings,\n\n" +
                               "You have received a new reservation request for your restaurant, " + restaurantName + ". Here are the details below:" +
                               "\n\nName: " + reservation.Name +
                               "\nPhone: " + reservation.Phone +
                               "\nReservation Time: " + reservation.ReservationTime.ToString("MMM dd, yyyy at hh:mm tt") +
                               "\nParty Size: " + reservation.PartySize +
                               "\nComment: " + reservation.Comment +
                               "\n\nReply soon by going to your dashboard.";

                objMail.To.Add(this.toAddress);
                objMail.From = (this.fromAddress);
                objMail.Subject = this.Subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendAcceptMail(Reservation reservation)
        {
            try
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                string restaurantName = restaurantDB.GetRestaurantNameByID(reservation.RestaurantID);

                this.Recipient = reservation.Email;
                this.Subject = "Accepted Reservation for " + reservation.Name;
                this.Message = "Congratulations " + reservation.Name + "," +
                               "\n\nYour reservation request for " + reservation.RestaurantName + " has been accepted! Here are the details below:" +
                               "\n\nName: " + reservation.Name +
                               "\nPhone: " + reservation.Phone +
                               "\nReservation Time: " + reservation.ReservationTime.ToString("MMM dd, yyyy at hh:mm tt") +
                               "\nParty Size: " + reservation.PartySize +
                               "\nComment: " + reservation.Comment +
                               "\n\nPlease arrive on time. If you have any questions contact the establishment directly.";

                objMail.To.Add(this.toAddress);
                objMail.From = (this.fromAddress);
                objMail.Subject = this.Subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendModifyMail(Reservation reservation)
        {
            try
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                string restaurantName = restaurantDB.GetRestaurantNameByID(reservation.RestaurantID);

                this.Recipient = reservation.Email;
                this.Subject = "Modified Reservation for " + reservation.Name;
                this.Message = "Greetings " + reservation.Name + "," +
                               "\n\nYour reservation request for " + reservation.RestaurantName + " has been modified. Here are the details below:" +
                               "\n\nName: " + reservation.Name +
                               "\nPhone: " + reservation.Phone +
                               "\nReservation Time: " + reservation.ReservationTime.ToString("MMM dd, yyyy at hh:mm tt") +
                               "\nParty Size: " + reservation.PartySize +
                               "\nComment: " + reservation.Comment +
                               "\n\nPlease arrive if you choose to accept. If you have changed your mind contact the establishment directly.";

                objMail.To.Add(this.toAddress);
                objMail.From = (this.fromAddress);
                objMail.Subject = this.Subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendDeclineMail(Reservation reservation)
        {
            try
            {
                RestaurantDB restaurantDB = new RestaurantDB();
                string restaurantName = restaurantDB.GetRestaurantNameByID(reservation.RestaurantID);

                this.Recipient = reservation.Email;
                this.Subject = "Declined Reservation for " + reservation.Name;
                this.Message = "Greetings " + reservation.Name + "," +
                               "\n\nYour reservation request for " + reservation.RestaurantName + " has unfortunately been declined. Here are the details below:" +
                               "\n\nName: " + reservation.Name +
                               "\nPhone: " + reservation.Phone +
                               "\nReservation Time: " + reservation.ReservationTime.ToString("MMM dd, yyyy at hh:mm tt") +
                               "\nParty Size: " + reservation.PartySize +
                               "\nComment: " + reservation.Comment +
                               "\n\nYou are welcomed to make a reservation for a different date. If you have any questions contact the establishment directly.";

                objMail.To.Add(this.toAddress);
                objMail.From = (this.fromAddress);
                objMail.Subject = this.Subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /**
        public void SendMail(String recipient, String sender, String subject, String body, String cc = "", String bcc = "")
        {
            try
            {
                this.Recipient = recipient;
                this.Sender = sender;
                this.Subject = subject;
                this.Message = body;

                objMail.To.Add(this.toAddress);
                objMail.From = this.fromAddress;
                objMail.Subject = this.subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                if (cc != null && !cc.Equals(String.Empty))
                {
                    this.CCAddress = cc;
                    objMail.CC.Add(this.ccAddress);
                }

                if (bcc != null && !bcc.Equals(String.Empty))
                {
                    this.BCCAddress = bcc;
                    objMail.Bcc.Add(this.bccAddress);
                }

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean SendMail()
        {
            try
            {
                objMail.To.Add(this.toAddress);
                objMail.From = this.fromAddress;
                objMail.Subject = this.subject;
                objMail.Body = this.messageBody;
                objMail.IsBodyHtml = this.isHTMLBody;
                objMail.Priority = this.priority;

                if (!ccAddress.Equals(String.Empty))
                    objMail.CC.Add(this.ccAddress);

                if (!bccAddress.Equals(String.Empty))
                    objMail.Bcc.Add(this.bccAddress);

                SmtpClient smtpMailClient = new SmtpClient(this.mailHost);
                smtpMailClient.Send(objMail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        **/
        public String Recipient
        {
            get { return this.toAddress.ToString(); }
            set { this.toAddress = new MailAddress(value); }
        }
        public String Sender
        {
            get { return this.fromAddress.ToString(); }
        }
        public String CCAddress
        {
            get { return this.ccAddress.ToString(); }
            set { this.ccAddress = new MailAddress(value); }
        }
        public String BCCAddress
        {
            get { return this.bccAddress.ToString(); }
            set { this.bccAddress = new MailAddress(value); }
        }
        public String Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }
        public String Message
        {
            get { return this.messageBody; }
            set { this.messageBody = value; }
        }
        public Boolean HTMLBody
        {
            get { return this.isHTMLBody; }
            set { this.isHTMLBody = value; }
        }
        public MailPriority Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }
        public String MailHost
        {
            get { return this.mailHost; }
            set { this.mailHost = value; }
        }
    }
}