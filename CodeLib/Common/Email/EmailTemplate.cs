using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeLib.Models;

namespace CodeLib.Email
{
    #region EmailTemplateInfo Class

    [Serializable]
    public class EmailTemplateInfo
    {
        public int EmailTemplateId { get; set; }
        public string Subject { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Body { get; set; }
    }

    #endregion

    [Serializable]
    public class EmailTemplate
    {
        // Tip: don't use short links in emails. It's a red flag for SPAM.

        #region Properties
        private static string AddContentToTemplate(string content)
        {
            string template = "<html xmlns='http://www.w3.org/1999/xhtml'><body style='font-size: 14px; font-family: sans-serif, Arial; color: #424242; -webkit-text-size-adjust: none; width: 100% !important; margin: 0; padding: 0;'><center><div style='max-width:730px; text-align: left; padding: 10px 0 20px 0;'><div style='background-color: #2A3232; padding:10px 15px; border-top-left-radius:5px; border-top-right-radius:5px;'><a href='" + CommonObjects.SITE_URL + "'><img src='[logoUrl].png'alt='logo'height='50px'width='250px'style='border: none;'/></a></div><div style='background-color:#f9f9f9; font-size: 14px; font-family: sans-serif, Arial; color: #333333; margin:0; padding:15px 10px 25px 12px; border: 2px solid #e8edf1; border-bottom:none;'>{{content}}</div><div style=\"padding:5px 15px 15px 15px; repeat-x; border:#ddd solid 1px; border-bottom-left-radius:5px; border-bottom-right-radius:5px;\"><div style='background-color:#E8E5E5; font-size: 14px; font-family: sans-serif, Arial; color: #333333; margin:0; padding:15px 10px 25px 12px; border: 2px solid #e8edf1;border-bottom-left-radius:5px; border-bottom-right-radius:5px;'>{{footer}}</div></div></div></center></body></html>";
            return template.Replace("{{content}}", content).Replace("{{footer}}", CommonObjects.EMAIL_FOOTER);
        }
        #endregion

        public static bool SendDefaultEmail(EmailInfo emailInfo)
        {
            bool sent = false;

            try
            {
                // Build the HTML message
                emailInfo.Body = AddContentToTemplate(emailInfo.Body);
                sent = Email.EmailSender.SendEmail(emailInfo);
            }
            catch (Exception ex)
            {
                DAL.CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null,
                    "Email failed to send. Error details: " + ex.Message, ex.StackTrace, emailInfo.Subject + ". [To] " + emailInfo.To);
            }

            return sent;
        }

        public static bool SendRegistrationEmail(AppUser user, string callBackUrl)
        {
            bool sent = false;

            try
            {
                string subject = "Welcome to " + CommonObjects.COMPANY_NAME + "!";
                string toEmail = (user.IdentityUser != null && !string.IsNullOrWhiteSpace(user.IdentityUser.Email)) ? user.IdentityUser.Email : user.Email;

                System.Text.StringBuilder sbBody = new System.Text.StringBuilder();
                sbBody.Append("<div style='margin-bottom:15px; font-size:18px; font-weight:bold;'>" + subject + "<br /><hr /></div>");
                sbBody.Append("Hi " + user.FirstName + ", <br/><br/>Thank you for sigining up for our online services. We're happy you joined us!");
                sbBody.Append("<br/><br/>Please <a href=\"" + callBackUrl + "\">confirm your email address</a> so you can login to your account.");
                sbBody.Append("<br /><br /><b>New Account Registration</b><br/>You received this message because an account was created in our system using the email address " + user.IdentityUser.Email);
                sbBody.Append(". &nbsp;If you didn't authorize the use of your Email address, please contact our <a href='" + CommonObjects.SUPPORT_URL + "'>Support Team</a> and we'll get things sorted out for you.");

                EmailInfo email = new EmailInfo(subject, CommonObjects.NOTIFY_EMAIL, CommonObjects.COMPANY_NAME,
                    toEmail, AddContentToTemplate(sbBody.ToString()));

                sent = EmailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                DAL.CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null,
                    "Email failed to send. Error details: " + ex.Message, ex.StackTrace, user.UserId.ToString());
            }

            return sent;
        }

        public static bool SendForgotPasswordEmail(ApplicationUser user, string callBackUrl)
        {
            bool sent = false;

            try
            {
                string subject = "Reset Your Password";

                string body = "<div style='margin-bottom:15px; font-size:18px; font-weight:bold;'>" + subject + "<br /><hr /></div>" +
                    "You recently requested to reset your password. You can reset your password by following the link below. " +
                    "If you no longer need to reset your password, you can ignore this message and your password will not be reset." +
                    "<br /><br /><a href='" + callBackUrl + "' target='_blank'>Reset My Password</a><br /><br />" +
                    "If you have any concerns, please contact our <a href='" + CommonObjects.SUPPORT_URL + "'>Support Team</a>.";

                EmailInfo Email = new EmailInfo("Reset Your Password", CommonObjects.NOTIFY_EMAIL,
                    CommonObjects.COMPANY_NAME, user.Email, AddContentToTemplate(body));

                sent = EmailSender.SendEmail(Email);
            }
            catch (Exception ex)
            {
                DAL.CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null,
                    "Email failed to send. Error details: " + ex.Message, ex.StackTrace, user.Email);
            }

            return sent;
        }

    }
}
