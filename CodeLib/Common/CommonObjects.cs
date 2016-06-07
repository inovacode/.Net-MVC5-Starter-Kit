using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeLib
{
    #region Public Enums

    public enum LookupTypeIdEnum : int
    {
        LogType = 1,
        AppUserStatus = 2,
        QueryDataType = 3
    }

    public enum DatabaseIdEnum : int
    {
        NONE = 0,

        // Log Types
        LogType_SiteEmail = 1,
        LogType_SiteException = 2,

        // App User Status
        [Display(Name = "Active")]
        UserStatus_Active = 3,
        [Display(Name = "Inactive")]
        UserStatus_Inactive = 4,

        // Query Data Types
        QueryDataType_AspNetRoles = 5,
        QueryDataType_LookupTable = 6,
        QueryDataType_GetUsersByRoleId = 7,
        QueryDataType_GetUserById = 8,
        QueryDataType_GetUserRoles = 9
    }

    public enum RoleIdEnum : int
    {
        Admin = 1,
        AppUser = 2
    }

    public enum MessageType
    {
        Error,
        Notify,
        Success,
        Warning
    }

    #endregion

    public class CommonObjects
    {
        #region Public Properties/Constants

        public const String COMPANY_NAME = "InovaCode Inc.";
        public static string SUPPORT_URL = SITE_URL + "/support";
        public static string ENVIRONMENT = System.Configuration.ConfigurationManager.AppSettings["ENVIRONMENT"].ToUpper();
        public static bool IsAppInPROD { get { return ENVIRONMENT == ENVIRONMENT_PROD; } }
        public const String ENVIRONMENT_PROD = "PROD";
        public const String ENVIRONMENT_TEST = "TEST";
        public const String ENVIRONMENT_DEV = "DEV";
        public const String ERROR_MSG_SUPPORT = "If this problem continues, please contact support to let us know about it.";
        public const string SES_APPUSER = "SES_APPUSER";

        // Email Settings
        public const String NOTIFY_EMAIL = "notify@{domain.com}";
        public const String SUPPORT_EMAIL = "support@{domain.com}";
        public const String EMAIL_PASS = "{emailPassword}";

        public static string EMAIL_FOOTER = "<div style='margin-top:10px; line-height:24px; float:left;'>Thank you,<br /><span style='font-weight:bold; font-style:italic; color:Gray;'>" + CommonObjects.COMPANY_NAME + "</span></div><div style='clear:both;'></div><div style='border-bottom:1px solid #B4B6B8; margin:20px 0 10px 0;'></div><span style='font-size:9px;'>This message is a service email related to your use of " + CommonObjects.COMPANY_NAME + ".  For general inquiries, or to request support with your account, please contact our <a href='" + CommonObjects.SUPPORT_URL + "'>Support Team</a>. Want to manage your notification settings? <a href='" + CommonObjects.SITE_URL + "/Account/Login'>Log in</a></span>";

        // ENVIRONMENT Site URLS
        public const String SITE_PROD_URL = @"http://{domain.com}";
        public static string SITE_URL
        {
            get
            {
                string siteUrl = SITE_PROD_URL; // Default

                switch (ENVIRONMENT)
                {
                    case ENVIRONMENT_TEST:
                        siteUrl = @"http://testsite.{domain.com}";
                        break;
                    case ENVIRONMENT_DEV:
                        siteUrl = @"http://localhost:{port}";
                        break;
                }

                return siteUrl;
            }
        }

        #endregion
    }
}
