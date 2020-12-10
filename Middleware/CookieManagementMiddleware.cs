using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using KonstantinHVACweb.BusinessLogic.Extensions;

namespace KonstantinHVACweb.Middleware
{
    public class CookieManagementMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieManagementMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            ManageCookies(context);
            await _next(context);
        }

        private static void ManageCookies(HttpContext context)
        {
            #region Session
            var sessionId = Guid.NewGuid().ToString();
            if (context.Request.Cookies.ContainsKey("session_id"))
            {
                try
                {
                    sessionId = context.Request.Cookies["session_id"].Decrypt();
                }
                catch
                {
                    // ignored
                }
            }

            if (!context.Items.ContainsKey("SessionId"))
                context.Items.Add("SessionId", sessionId);

            context.Response.Cookies.Append("session_id", sessionId.Encrypt(),
                new CookieOptions { Expires = DateTime.Now.AddHours(1), HttpOnly = true });
            #endregion
        }
    }
}