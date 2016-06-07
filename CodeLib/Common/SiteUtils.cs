using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace CodeLib
{
    public static class SiteUtils
    {
        #region Common Methods

        public static Models.AppUser GetSessionUser()
        {
            Models.AppUser user = null;

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                user = (HttpContext.Current.Session[CommonObjects.SES_APPUSER] != null) ? (Models.AppUser)HttpContext.Current.Session[CommonObjects.SES_APPUSER] : null;

                if (user == null || user.UserId <= 0)
                {
                    user = DAL.UserDAL.GetUserById(HttpContext.Current.User.Identity.Name);
                    HttpContext.Current.Session[CommonObjects.SES_APPUSER] = user;
                }
            }

            return user;
        }

        public static void ClearLoginSession()
        {
            HttpContext.Current.Session[CommonObjects.SES_APPUSER] = null;

            System.Web.Security.FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static string GetLoadingIndicatorHtml(List<KeyValuePair<string, string>> elements)
        {
            return GetLoadingIndicatorHtml(elements, "/Scripts/spin.min.js");
        }
        public static string GetLoadingIndicatorHtml(List<KeyValuePair<string, string>> elements, string jsFilePath)
        {   
            StringBuilder html = new StringBuilder(@"
                <div id='loading'>
                    <div id='loadingcontent'>
                        <p id='loadingspinner'></p>
                    </div>
                </div>
                <script type='text/javascript' src='" + jsFilePath + @"'></script>
                <script type='text/javascript'>");

            foreach (KeyValuePair<string, string> element in elements)
            {
                html.Append(@"
                    $(function () {
                        $('#{{buttonId}}').click(function () {
                            $('#loading').fadeIn();
                            var opts = {
                                lines: 12, // The number of lines to draw
                                length: 7, // The length of each line
                                width: 4, // The line thickness
                                radius: 10, // The radius of the inner circle
                                color: '#000', // #rgb or #rrggbb
                                speed: 1, // Rounds per second
                                trail: 60, // Afterglow percentage
                                shadow: false, // Whether to render a shadow
                                hwaccel: false // Whether to use hardware acceleration
                            };

                            var target = document.getElementById('loading');
                            var spinner = new Spinner(opts).spin(target);
                        });
                    });

                    $('#{{formId}}').bind('invalid-form.validate', function () {
                        var spinner = new Spinner().stop();
                        document.getElementById('loading').style.display = 'none';
                    });

                ".Replace("{{formId}}", element.Key).Replace("{{buttonId}}", element.Value));
            }

            html.Append("</script>");

            return html.ToString();
        }

        public static string GetPageName()
        {
            try
            {
                return HttpContext.Current.Request.ServerVariables["PATH_INFO"].ToString();
            }
            catch { return null; } // Not a show-stopper
        }

        public static string GetDisplayAttributeFrom(this Enum enumValue, Type enumType)
        {
            return enumType.GetMember(enumValue.ToString()).First()
                .GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>().Name;
        }

        #region Message Methods

        public static string FormatMessage(string message, MessageType msgType)
        {
            switch (msgType)
            {
                case MessageType.Error:
                    message = "<span style='color:#C82E2E;'>" + message + "</span>";
                    break;
            }

            return message;
        }

        #endregion

        #endregion
    }
}
