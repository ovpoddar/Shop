using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace Shop.Extensions
{
    public static class BrowserCookiesSupportExtensions
    {
        public static void CheckSameSite(this AppendCookieContext context)
        {
            if (context.CookieOptions.SameSite == SameSiteMode.None)
            {
                var userAgent = context.Context.Request.Headers["User-Agent"].ToString();

                context.CookieOptions.Expires = DateAndTime.Now.AddYears(1);

                if (DisallowsSameSiteNone(userAgent))
                    context.CookieOptions.SameSite = SameSiteMode.Unspecified;
            }
        }

        public static void CheckSameSite(this DeleteCookieContext context)
        {
            if (context.CookieOptions.SameSite == SameSiteMode.None)
            {
                var userAgent = context.Context.Request.Headers["User-Agent"].ToString();

                context.CookieOptions.Expires = DateAndTime.Now;

                if (DisallowsSameSiteNone(userAgent))
                    context.CookieOptions.SameSite = SameSiteMode.Unspecified;
            }
        }

        public static bool DisallowsSameSiteNone(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return false;
            if (userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12"))
                return true;
            if (userAgent.Contains("Safari") && userAgent.Contains("Macintosh Intel Mac OS X 10_14") && userAgent.Contains("Version/"))
                return true;
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
                return true;
            return false;
        }
    }
}
