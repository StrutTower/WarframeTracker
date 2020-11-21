using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Website.Utilities;

namespace Website.TagHelpers {
    [HtmlTargetElement("div", Attributes = "display-for")]
    [HtmlTargetElement("span", Attributes = "display-for")]
    [HtmlTargetElement("a", Attributes = "display-for")]
    [HtmlTargetElement("abbr", Attributes = "display-for")]
    [HtmlTargetElement("p", Attributes = "display-for")]
    [HtmlTargetElement("em", Attributes = "display-for")]
    [HtmlTargetElement("strong", Attributes = "display-for")]
    [HtmlTargetElement("small", Attributes = "display-for")]
    [HtmlTargetElement("th", Attributes = "display-for")]
    [HtmlTargetElement("td", Attributes = "display-for")]
    [HtmlTargetElement("h1", Attributes = "display-for")]
    [HtmlTargetElement("h2", Attributes = "display-for")]
    [HtmlTargetElement("h3", Attributes = "display-for")]
    [HtmlTargetElement("h4", Attributes = "display-for")]
    [HtmlTargetElement("h5", Attributes = "display-for")]
    [HtmlTargetElement("h6", Attributes = "display-for")]
    [HtmlTargetElement("li", Attributes = "display-for")]
    public class DisplayForTagHelper : TagHelper {
        public DisplayForTagHelper(IHtmlHelper htmlHelper) {
            HtmlHelper = htmlHelper;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }
        private IHtmlHelper HtmlHelper { get; set; }

        /// <summary>
        /// Model to display
        /// </summary>
        [HtmlAttributeName("display-for")]
        public ModelExpression Model { get; set; }

        /// <summary>
        /// Optional. Name of the template to use
        /// </summary>
        [HtmlAttributeName("display-template")]
        public string TemplateName { get; set; }

        /// <summary>
        /// Set to true to append the display template to the end of any existing html in the element. Default is false.
        /// </summary>
        [HtmlAttributeName("asp-append")]
        public bool Append { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            ((IViewContextAware)HtmlHelper).Contextualize(ViewContext);
            if (Append) {
                output.PostContent.AppendHtml(HtmlHelper.TagHelperDisplay(Model, TemplateName));
            } else {
                output.PreContent.AppendHtml(HtmlHelper.TagHelperDisplay(Model, TemplateName));
            }
        }
    }
}
