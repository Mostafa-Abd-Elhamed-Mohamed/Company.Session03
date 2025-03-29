using System.Net;
using System.Net.Mail;

namespace Company.Session03.PL.Helper
{
    public class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("mostafaabdelhamed112004@gmail.com", "vkoiixyxxwhisjoa");
                // vkoiixyxxwhisjoa
                client.Send("mostafaabdelhamed112004@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
