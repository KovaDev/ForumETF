using System;
using System.Text.RegularExpressions;
using System.Web;

namespace ForumETF.HtmlHelpers
{
    public class HelperMethods
    {
        public static string GenerateAbsolutUri(string relativePath)
        {
            var path = VirtualPathUtility.ToAbsolute(relativePath);

            return new Uri(HttpContext.Current.Request.Url, path).AbsoluteUri;
        }

        public static string GetUrlSeoName(string name)
        {
            return Regex.Replace(name.ToLower().Replace(@"'", String.Empty), @"[^\w]+", "-");
        }
    }
}