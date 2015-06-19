using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailTestApp
{
    public class Emailer
    {
        private readonly string Environment = "Email Test";


        public void AlertUsers(Func<string[]> getEmailsFunc, string subject, string body, IEnumerable<string> attachments, bool doDispose = true)
        {

            if (attachments == null) attachments = new string[] { };

            //add the environment location
            subject += string.Format(" ({0})", Environment);

            //get 'admin' emails
            string[] emails = getEmailsFunc.Invoke();

            //create the attachment, if present
            IEnumerable<Attachment> files = attachments.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => new Attachment(s));

            //send emails
            foreach (string email in emails)
            {
                string emailInClosureThanksToResharper = email;
                string message = "EmailSent: (" + email + ")" + "\r\n" + subject + "\r\n" +
                    body.Replace("<br />", "\r\n");

                MyLogger.Info(message);

                try
                {
                   // Retry.Do(() =>
                    SendEmail(subject, body, emailInClosureThanksToResharper, files, doDispose);
                    //, new TimeSpan(0, 0, 0, 15));
                }
                catch (AggregateException ae)
                {
                    ae.Handle((x) =>
                    {
                        MyLogger.Error("(Retry) Email failed to send to " + emailInClosureThanksToResharper, x);
                        return true;
                    });
                }
            }
        }

        public async void AlertUsersAsync(Func<string[]> getEmailsFunc, string subject, string body, IEnumerable<string> attachments, bool doDispose = true)
        {
            //Note: this and the code around it is probably horrible "async" programming by .NET 4.5 standards.  
            //  Please do not follow it as a pattern - EMR 1/6/2015
            // Note also that there's actually an SmtpClient.SendAsync() function we could be using.
            await Task.Run(() => AlertUsers(getEmailsFunc, subject, body, attachments, doDispose));
        }

        public void SendEmail(string subject, string body, string emailAddress, IEnumerable<Attachment> fileAttachments = null, bool doDispose = true)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(emailAddress));
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            if (fileAttachments != null)
                foreach (Attachment a in fileAttachments)
                    mail.Attachments.Add(a);

            var smtpServer = new SmtpClient();

            //default is 100,000.  The lower value is useful for testing.
            smtpServer.Timeout = 10000; 

            smtpServer.Send(mail);

            //dispose our mail message which should release file access to the attachments
            if (doDispose)
                mail.Dispose();
        }
    }
}