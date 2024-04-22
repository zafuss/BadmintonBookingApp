using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
namespace BadmintonBookingApp.Helpers
{
    public class SendMail
    {
        public static bool SendEmail(string to, string subject, string body, string? attachFile)
        {
            try
            {

                MailMessage msg = new MailMessage(ConstantHelpers.emailSender, to, subject, body);
                msg.IsBodyHtml = true;  
                using (var client = new SmtpClient(ConstantHelpers.hostEmail, ConstantHelpers.portEmail))
                {
                    
                    client.EnableSsl = true;
                    if (!string.IsNullOrEmpty(attachFile))
                    {
                        Attachment attachment = new Attachment(attachFile); 
                        msg.Attachments.Add(attachment);
                    }
                    NetworkCredential credential = new NetworkCredential(ConstantHelpers.emailSender, ConstantHelpers.passwordSender);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    client.Send(msg);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}