using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using aymk_engine.Properties;
using aymk_engine.Model;
using aymk_engine.Model.Custom;

namespace aymk_engine.Business
{
    public class MailBL
    {
        private static MailBL instance;
        private static SmtpClient client;
        private static MailAddress fromAddress;
        public static MailBL GetInstance()
        {
            if (instance == null)
                instance = new MailBL();

            client = new SmtpClient(Settings.Default.Mail_Server,Settings.Default.Mail_Port);             
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;


            fromAddress = new MailAddress(Settings.Default.Mail_Address, "AYMK Coin Order Management System", Encoding.UTF8);

            return instance;
        }

        public Response SendMail(string to, string subject,string body, string displayName = "")
        {
            try
            {
                client.Credentials = new System.Net.NetworkCredential(Settings.Default.Mail_Address, Settings.Default.Mail_Password);

                if (displayName == "")
                    displayName = to;

                MailAddress toAddress = new MailAddress(to, displayName, Encoding.UTF8);

                using (MailMessage mail = new MailMessage(fromAddress, toAddress))
                {
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    mail.Body = body;
                    mail.Subject = subject;
                    client.Send(mail);
                }


                Console.WriteLine(subject);
                return new Response();
               
                
            }
            catch (Exception ex)
            {
                return new Response(ex.Message);
            }            

        }
 
    }


}
