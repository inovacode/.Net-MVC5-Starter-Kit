#region Using Directives
using System;
using System.Net;
using System.Net.Mail;
using System.Linq;

using CodeLib;
#endregion

namespace CodeLib.Email
{
    [Serializable]
    public class EmailInfo
    {
        #region Public Properties

        public string Subject { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public MailPriority Priority { get; set; }
        public SmtpClient SmtpClientSettings { get; set; }
        public CodeLib.DatabaseIdEnum LogTypeId
        {
            get
            {
                return CodeLib.DatabaseIdEnum.LogType_SiteEmail;
            }
        }

        #endregion

        #region Constructors

        public EmailInfo() { } // Empty Constructor

        public EmailInfo(string sSubject, string sFrom, string sFromName, string sTo, string sBody)
        {
            this.Subject = sSubject;
            this.From = sFrom;
            if (!string.IsNullOrWhiteSpace(sFromName))
                this.FromName = sFromName;
            this.To = sTo;
            this.Body = sBody;
            this.IsBodyHtml = true;
            this.Priority = MailPriority.Normal;
        }

        public EmailInfo(string sSubject, string sFrom, string sFromName, string sTo, string sCC, string sBCC,
             string sBody, bool bIsHtml, MailPriority priority)
        {
            this.Subject = sSubject;
            this.From = sFrom;
            if (!string.IsNullOrWhiteSpace(sFromName))
                this.FromName = sFromName;
            this.To = sTo;
            this.CC = sCC;
            this.BCC = sBCC;
            this.Body = sBody;
            this.IsBodyHtml = bIsHtml;
            this.Priority = priority;
        }

        #endregion

        #region Public Methods

        public bool Send(EmailInfo email)
        {
            return EmailSender.SendEmail(email);
        }

        /// <summary>
        /// A method to insert email log info
        /// </summary>
        public static void InsertEmailLog(string emailSubject, string emailTo, string emailFrom, string emailBody)
        {
            // Note: don't change this description format in case other processes query it to verify if an email has been sent
            string desc = "Subject: " + emailSubject + ", Email To: " + emailTo + ", Email From: " + emailFrom + ", Email Body: " + emailBody;

            using (var dbContext = new CodeLib.Models.Entities())
            {
                dbContext.Log_Insert((int)CodeLib.DatabaseIdEnum.LogType_SiteEmail, desc, null, null, emailTo);
            }
        }

        #endregion
    }
}
