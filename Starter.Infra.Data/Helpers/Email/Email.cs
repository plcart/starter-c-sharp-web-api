using System.Net.Mail;

namespace Starter.Infra.Data.Helpers
{
    public class Email
    {
        public static void SendEmail(string subject,string body,bool isHtml,params string[] to)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress("you@yourcompany.com","Your Company");
            foreach (var item in to)
            {
                mail.To.Add(item);
            }
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isHtml;
            client.Send(mail);
        }
    }
}
