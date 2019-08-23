using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Website.TagHelpers {
    [HtmlTargetElement("label", Attributes = "twr-for", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TwrLabelTagHelper : LabelTagHelper {
        public TwrLabelTagHelper(IHtmlGenerator htmlGenerator) : base(htmlGenerator) { }

        [HtmlAttributeName("twr-for")]
        public ModelExpression TwrFor {
            get => For;
            set => For = value;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            base.Process(context, output);

            output.AddClass("twr-label", HtmlEncoder.Default);
        }
    }
}
