using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BuyMeABible.Services
{
    public static class EmailUtility
    {
        public static string SendMsg(string from, string to, string subject, string body, string replyTo)
        {
            string result = "";
            try
            {
                MailMessage message = new MailMessage(from, to, subject, body);
                message.ReplyToList.Add(replyTo);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient SMTPServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                SMTPServer.UseDefaultCredentials = true;
                SMTPServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                var enablessl = bool.Parse(ConfigurationManager.AppSettings["SMTPEnableSSL"]);
                SMTPServer.EnableSsl = enablessl;

                SMTPServer.Send(message);
                message.Dispose();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}