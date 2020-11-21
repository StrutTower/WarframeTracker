using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Website.TagHelpers {
    public class DescriptionTagHelper : TagHelper {
        [HtmlAttributeName("asp-for")]
        public ModelExpression Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if (!string.IsNullOrWhiteSpace(Model.Metadata.Description)) {
                output.TagName = "div";
                output.Attributes.Add("class", "form-help-text");
                output.Content.Append(Model.Metadata.Description);
            }
            base.Process(context, output);
        }
    }
}
