using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.TagHelpers {
    public class HelpTextTagHelper : TagHelper {
        [HtmlAttributeName("twr-for")]
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
