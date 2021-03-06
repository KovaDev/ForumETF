﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
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
            //var name = ExpressionHelper.GetExpressionText(expression);
            //var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            //string htmlString = 

            return null;
        }

        public static IHtmlString ProfileImageFor(this HtmlHelper helper, string content, object htmlAttributes)
        {
            var img = new TagBuilder("img");
            //html.MergeAttribute("class", "img-rounded");
            //html.MergeAttribute("id", "profilePicture");
            //html.MergeAttribute("alt", "profile-pic");

            string url = "/Uploads/ProfilePictures/" + Path.GetFileName(content);

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                var value = prop.GetValue(htmlAttributes);
                if (value != null)
                    img.MergeAttribute(prop.Name.Replace("_", "-"), value.ToString(), true);
            }

            if (String.IsNullOrEmpty(content) && String.IsNullOrWhiteSpace(content))
            {
                img.MergeAttribute("src", "http://www.sdtn.com/files/pictures/sdtn_default_profile_image.jpg");
            }
            else
            {
                img.MergeAttribute("src", HelperMethods.GenerateAbsolutUri(url));
            }

            return new HtmlString(img.ToString());
        }

        public static IHtmlString SubstringTextFor(this HtmlHelper helper, string text, int textLength, string action, string controller, object routeValues, string htmlAttributes)
        {
            var link = new TagBuilder("a");
            var href = "/" + controller + "/" + action;

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                var value = prop.GetValue(htmlAttributes);
                if (value != null)
                    link.MergeAttribute(prop.Name.Replace("_", "-"), value.ToString(), true);
            }

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(routeValues))
            {
                var value = prop.GetValue(routeValues);
                if (value != null)
                    href += "/" + Convert.ToInt32(value);
            }

            var linkText = text.Length > textLength ? text.Substring(0, textLength) + " ..." : text;

            link.MergeAttribute("href", href);
            link.InnerHtml = linkText;

            return new HtmlString(link.ToString());
        }

    }
}