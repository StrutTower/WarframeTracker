using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Website.Infrastructure;

namespace Website.TagHelpers {
    [HtmlTargetElement("*", Attributes = "display-for")]
    public class DisplayForTagHelper : TagHelper {
        public DisplayForTagHelper(IHtmlHelper htmlHelper) {
            _htmlHelper = htmlHelper;
        }

        private IHtmlHelper _htmlHelper { get; set; }

        [HtmlAttributeName("display-for")]
        public ModelExpression Model { get; set; }

        [HtmlAttributeName("display-template")]
        public string TemplateName { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);
            output.PostContent.AppendHtml(_htmlHelper.Display(Model, TemplateName));
        }
    }
}
