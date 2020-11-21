using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

namespace Website.Utilities {
    public static class HtmlHelperDisplayExtensions {
        /// <summary>
        /// Returns the DisplayAttribute Description field
        /// </summary>
        /// <param name="expression">Model</param>
        public static IHtmlContent DescriptionFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            object descAttr = memberExpression.Member.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            if (descAttr != null) {
                DescriptionAttribute attr = (DescriptionAttribute)descAttr;
                if (!string.IsNullOrWhiteSpace(attr.Description))
                    return new HtmlString($"<div class=\"form-help-text\">{attr.Description}</div>");
            }

            object disAttr = memberExpression.Member.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();
            if (disAttr != null) {
                DisplayAttribute attr = (DisplayAttribute)disAttr;
                if (!string.IsNullOrWhiteSpace(attr.Description))
                    return new HtmlString($"<div class=\"form-help-text\">{attr.Description}</div>");
            }

            return HtmlString.Empty;
        }

        #region Helpers
        public static string ToRawString(this IHtmlContent htmlContent) {
            using StringWriter writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
        #endregion
    }
}
