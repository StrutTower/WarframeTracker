using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Website.TagHelpers {
    [HtmlTargetElement("input", Attributes = "twr-for", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TwrInputTagHelper : InputTagHelper {
        public TwrInputTagHelper(IHtmlGenerator htmlGenerator) : base(htmlGenerator) { }

        [HtmlAttributeName("twr-for")]
        public ModelExpression TwrFor {
            get => For;
            set => For = value;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            base.Process(context, output);

            List<string> standardTypes = new List<string> { "text", "number", "password" };

            if (standardTypes.Contains(output.Attributes["type"].Value)) {
                output.AddClass("twr-input", HtmlEncoder.Default);
            }

            //if (output.Attributes["type"].Value.ToString() == "checkbox") {
            //    output.WriteTo()
            //}

            //var fullName = NameAndIdProvider.GetFullHtmlFieldName(ViewContext, For.Name);
            
            //if (ViewContext.ViewData.ModelState.TryGetValue(fullName, out var entry) && entry.Errors.Count > 0) {
            //    output.AddClass("is-invalid", HtmlEncoder.Default);
            //    output.RemoveClass("input-validation-error", HtmlEncoder.Default);
            //}
        }
    }
}
