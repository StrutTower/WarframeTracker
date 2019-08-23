using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;

namespace Website.Infrastructure {
    public static class HtmlHelperExtensions {
        /// <summary>
        /// Returns a label, editor, and validation message for the specified model property within a form-group div.
        /// This is the same as LabelEditorValFor except that it will be inside a form-group div.
        /// </summary>
        /// <param name="expression">Model</param>
        /// <param name="labelDisplay">Optional. Overrides the default label text.</param>
        /// <param name="editorTemplateName">Optional. Name of the editor template to use.</param>
        /// <param name="editorViewData">Optional. ViewData to send to the editor template.</param>
        public static IHtmlContent FormGroupEditorFor<TModel, TValue>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string labelDisplay = null,
            string editorTemplateName = null,
            object htmlAttributes = null) {

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("form-group");
            div.InnerHtml.AppendHtml(LabelEditorValFor(html, expression, labelDisplay, editorTemplateName, htmlAttributes).ToString());

            return div;
        }

        /// <summary>
        /// Returns a label, editor, and validation message for the specified model property.
        /// </summary>
        /// <param name="expression">Model</param>
        /// <param name="labelDisplay">Optional. Overrides the default label text.</param>
        /// <param name="editorTemplateName">Optional. Name of the editor template to use.</param>
        /// <param name="editorViewData">Optional. ViewData to send to the editor template.</param>
        public static IHtmlContent LabelEditorValFor<TModel, TValue>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string labelDisplay = null,
            string editorTemplateName = null,
            object htmlAttributes = null) {

            // Bootstrap 4 form-check for Booleans
            if (typeof(TValue) == typeof(bool) && string.IsNullOrWhiteSpace(editorTemplateName)) {
                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("form-check");
                div.InnerHtml.AppendHtml(html.EditorFor(expression, null, new { htmlAttributes }).ToRawString() + html.LabelFor(expression, labelDisplay).ToRawString());
                return new HtmlString(
                    div.ToRawString() +
                    html.HelpTextFor(expression).ToRawString() +
                    html.ValidationMessageFor(expression).ToRawString()
                );
            }

            TagBuilder twrInput = new TagBuilder("div");
            twrInput.AddCssClass("twr-input");

            twrInput.InnerHtml.AppendHtml(html.EditorFor(expression, editorTemplateName, new { htmlAttributes }).ToRawString() + html.LabelFor(expression, labelDisplay).ToRawString());

            return new HtmlString(
                twrInput.ToRawString() +
                html.HelpTextFor(expression).ToRawString() +
                html.ValidationMessageFor(expression).ToRawString());
        }

        public static IHtmlContent HelpTextFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
            string description = modelExplorer.Metadata.Description;

            if (string.IsNullOrWhiteSpace(description)) return HtmlString.Empty;
            return new HtmlString(string.Format("<div class=\"form-help-text\">{0}</div>", description));
        }


        /// <summary>
        /// Convert the boolean to an icon
        /// </summary>
        /// <param name="boolean">Boolean to display as an icon</param>
        public static IHtmlContent BooleanIcon(this IHtmlHelper html, bool boolean) {
            TagBuilder tag = new TagBuilder("span");
            if (boolean) {
                tag.AddCssClass("mdi mdi-check text-success");
            } else {
                tag.AddCssClass("mdi mdi-cancel text-danger");
            }
            return tag;
        }

        public static IHtmlContent WebsiteVersion(this IHtmlHelper html) {
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            return new HtmlString(string.Join(".", version.Split('.').Take(3)));
        }

        private static string ToRawString(this IHtmlContent htmlContent) {
            using (StringWriter writer = new StringWriter()) {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
