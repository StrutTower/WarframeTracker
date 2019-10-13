using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Website.TagHelpers {
    [HtmlTargetElement("*", Attributes = "display-for")]
    public class TwrDisplayForTagHelper : TagHelper {
        public TwrDisplayForTagHelper(IHtmlHelper htmlHelper) {
            _htmlHelper = htmlHelper;
        }

        [HtmlAttributeName("display-for")]
        public ModelExpression Model { get; set; }

        [HtmlAttributeName("display-template")]
        public string TemplateName { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private IHtmlHelper _htmlHelper;
        private IHtmlHelper HtmlHelper {
            get {
                (_htmlHelper as IViewContextAware)?.Contextualize(ViewContext);
                return _htmlHelper;
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var type = Model.Metadata.UnderlyingOrModelType;

            if (type.IsPrimitive && string.IsNullOrWhiteSpace(TemplateName)) {
                output.Content.SetContent(Model.ModelExplorer.GetSimpleDisplayText());
            } else {
                var htmlContent = HtmlHelper.GetHtmlContent(Model, TemplateName);
                output.Content.SetHtmlContent(htmlContent);
            }
        }

    }

    static class HelperExtensions {
        public static IHtmlContent GetHtmlContent(this IHtmlHelper htmlHelper, ModelExpression expression, string templateName = null) {
            var viewEngine = htmlHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var bufferScope = htmlHelper.GetFieldValue<IViewBufferScope>();
            var htmlContent = new TemplateBuilder(viewEngine, bufferScope, htmlHelper.ViewContext, htmlHelper.ViewContext.ViewData, expression.ModelExplorer, expression.Name, templateName, true, null).Build();
            return htmlContent;
        }

        public static TValue GetFieldValue<TValue>(this object instance) {
            var type = instance.GetType();
            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                .FirstOrDefault(x => typeof(TValue).IsAssignableFrom(x.FieldType));
            return (TValue)field?.GetValue(instance);
        }
    }
}
