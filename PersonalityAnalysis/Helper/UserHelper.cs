using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PersonalityAnalysis.Helper
{
    public static class UserHelper
    {
        public static string GetUserData()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = id.Ticket;
                var userInfo = id.Ticket.UserData;               
            return userInfo;
            }
            return "";
        }

        public static string GetUserID()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = id.Ticket;
                var userName = id.Ticket.Name;
                return userName;
            }
            return "";
        }

        public static HttpCookie SetCookie(string userID,string userName)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        userID,
                        DateTime.UtcNow,
                        DateTime.UtcNow.AddMinutes(90),
                        isPersistent: true,
                        userData: userName,
                        cookiePath: FormsAuthentication.FormsCookiePath
                        );
            string encTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                Secure = true,
                HttpOnly = true
            };
            return cookie;
        }
    }
}