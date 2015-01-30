using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.HtmlHelpers
{
    public static class CustomUriHelpers
    {
        public static string FormatAbsolutUrl(this Uri url, string path)
        {
            return String.Format("{0}/{1}", url.FormatUrlStart(), path.TrimStart('/'));
        }

        public static string FormatUrlStart(this Uri url)
        {
            return String.Format("{0}://{1}{2}", url.Scheme, url.Host, url.Port == 80 ? string.Empty :  ":" + url.Port );
        }
    }
}