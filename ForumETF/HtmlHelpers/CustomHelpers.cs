using System;
using System.Collections.Generic;
using System.Linq;
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
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            //string htmlString = 

            return null;
        }
    }
}