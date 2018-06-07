using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace WebAppEC.Models
{
    public class Email
    {
        public void DispararEmail(string emailDestino, string texto)
        {
            string GmailUserName = "gandalfocinzentooubranco@gmail.com";
            string GmailPassword = "!1gandalf1!";
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUserName, GmailPassword);
            using (var message = new MailMessage(GmailUserName, emailDestino))
            {
                message.Subject = "Esqueci Minha Senha";
                message.Body = "SUA SENHA: " + texto;
                message.IsBodyHtml = true;
                smtp.Send(message);
            }
        }
    }
}