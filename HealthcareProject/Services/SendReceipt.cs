using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HealthcareProject.Services
{
    public class SendReceipt
    {
        public void Sendemail(string receiptemail, double amount, int billingid)
        {
            //Send an email
            //Sending email, make a different class file for this.
            var fromAddress = new MailAddress("phunyal.utsav1@gmail.com", "Healthcare Service");
            var toAddress = new MailAddress(receiptemail);
            string messagebody = "You paid your invoice of amount " + amount + " and billing Id " + billingid;
            const string subject = "Receipt of Paymment";
            string body = messagebody;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, "xrtwdhjqxqvsurfn")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
