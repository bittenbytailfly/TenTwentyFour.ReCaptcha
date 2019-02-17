using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Mvc;

namespace TenTwentyFour.ReCaptcha.HtmlHelpers
{
    public enum ReCaptchaTheme { Dark, Light }

    public static class ReCaptchaHelpers
    {
        public static MvcHtmlString ReCaptchaHeadScript(this HtmlHelper helper)
        {
            return MvcHtmlString.Create("<script src='https://www.google.com/recaptcha/api.js'></script>");
        }

        /// <summary>
        /// This one uses the configuration key in the web.config
        /// </summary>
        public static MvcHtmlString ReCaptcha(this HtmlHelper helper, ReCaptchaTheme theme, int tabIndex)
        {
            return ReCaptcha(helper, ConfigurationManager.AppSettings[ConfigurationKeys.SiteKey], theme, tabIndex);
        }

        public static MvcHtmlString ReCaptcha(this HtmlHelper helper, string siteKey, ReCaptchaTheme theme, int tabIndex)
        {
            return MvcHtmlString.Create(String.Format("<div class=\"g-recaptcha\" data-sitekey=\"{0}\" data-theme=\"{1}\" data-tabindex=\"{2}\"></div>", siteKey, theme.ToString().ToLower(), tabIndex));
        }
    }
}
