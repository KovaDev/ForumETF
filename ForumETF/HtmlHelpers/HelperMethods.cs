using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}