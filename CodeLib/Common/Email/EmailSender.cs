#region Using Directives
using System;
using System.Net;
using System.Net.Mail;

using CodeLib;
#endregion

namespace CodeLib.Email
{
    public class EmailSender
    {
        public static bool SendEmail(EmailInfo email)
        {
			string fromEmail;
			string errorMsg;
			return SendEmail(email, false, out fromEmail, out errorMsg);
        }
        public static bool SendEmail(EmailInfo email, bool queueEmail)
        {
			string fromEmail;
            string errorMsg;
            return SendEmail(email, queueEmail, out fromEmail, out errorMsg);
        }

		public static bool SendEmail(EmailInfo email, out string fromEmail)
		{
			string errorMsg;
			return SendEmail(email, false, out fromEmail, out errorMsg);
		}

		public static bool SendEmail(EmailInfo email, bool queueEmail, out string fromEmail, out string errorMsg)
        {
			fromEmail = null;
			errorMsg = null;

            try
            {
                MailMessage mMailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(email.FromName))
                    mMailMessage.From = new MailAddress(email.From, email.FromName);
                else
                    mMailMessage.From = new MailAddress(email.From);

				if (!string.IsNullOrWhiteSpace(email.To) || !string.IsNullOrWhiteSpace(email.CC) || !string.IsNullOrWhiteSpace(email.BCC))
				{
                    if (!string.IsNullOrWhiteSpace(email.To))
                        mMailMessage.To.Add(new MailAddress(email.To.TrimEnd(',')));
					if (!string.IsNullOrWhiteSpace(email.CC))
						mMailMessage.CC.Add(new MailAddress(email.CC.TrimEnd(',')));
					if (!string.IsNullOrWhiteSpace(email.BCC))
					{
						if (email.BCC.IndexOf(',') > 0)
							mMailMessage.Bcc.Add(email.BCC.TrimEnd(','));
						else
                            mMailMessage.Bcc.Add(new MailAddress(email.BCC.TrimEnd(',')));
					}
				}
				else
					return false; // The email needs to be sent to somebody.  :-)

                mMailMessage.Subject = email.Subject;
                mMailMessage.Body = email.Body;
                mMailMessage.IsBodyHtml = email.IsBodyHtml;
                mMailMessage.Priority = MailPriority.Normal;

                SmtpClient smtpClient = null;
                if (email.SmtpClientSettings != null && !string.IsNullOrWhiteSpace(email.SmtpClientSettings.Host))
                    smtpClient = email.SmtpClientSettings;
                else
                {
                    // Google Apps
                    smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(email.From, CommonObjects.EMAIL_PASS),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = true,
                        Port = 587
                    };
                }

                if (queueEmail)
                {
                    // ToDo: Get PickupDirectoryPath on server
                    smtpClient.PickupDirectoryLocation = null; //pickupDirectoryPath;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                }
                
                smtpClient.Send(mMailMessage);
				fromEmail = email.From;

                // Log Email Sent.
                EmailInfo.InsertEmailLog(email.Subject, email.To, email.From, email.Body);

                return true;
            }
            catch (System.Net.Mail.SmtpException emEx)
			{
				errorMsg = emEx.Message;
                DAL.CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, emEx.Message, emEx.StackTrace, email.To);
                return false;
            }
            catch (Exception ex)
            {
				errorMsg = ex.Message;
                DAL.CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, ex.Message, ex.StackTrace, email.To);
                return false;
            }
        }
    }
}
