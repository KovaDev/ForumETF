using System;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.HtmlHelpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString ImageFor(this HtmlHelper htmlString, string source, string altText)
        {
            var builder = new TagBuilder("image");

            builder.MergeAttribute("src", source);
            builder.MergeAttribute("alt", altText);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString LabelWithMark(this HtmlHelper helper, string content)
        {
            string htmlString = String.Format("<label><mark>{0}</mark></label>", content);

            return new HtmlString(htmlString);
        }

        public static IHtmlString RichEditorFor<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            //string htmlString = 

            return null;
        }

        public static IHtmlString ProfileImageFor(this HtmlHelper helper, string content)
        {
            var html = new TagBuilder("img");
            html.MergeAttribute("class", "img-rounded");
            html.MergeAttribute("id", "profilePicture");
            html.MergeAttribute("alt", "profile-pic");

            string url = "/Uploads/ProfilePictures/" + System.IO.Path.GetFileName(content);

            if (String.IsNullOrEmpty(content.ToString()) && String.IsNullOrWhiteSpace(content.ToString()))
            {
                html.MergeAttribute("src", "http://www.sdtn.com/files/pictures/sdtn_default_profile_image.jpg");
            }
            else
            {
                html.MergeAttribute("src", HelperMethods.GenerateAbsolutUri(url));
            }

            return new HtmlString(html.ToString());
        }

    }
}