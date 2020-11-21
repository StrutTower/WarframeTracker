using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WesternStateHospital.WSHUtilities;

namespace Website.Utilities {
    public static class ViewUtilities {
        /// <summary>
        /// Convert the boolean to an icon.
        /// </summary>
        /// <param name="boolean">Boolean to display as an icon</param>
        public static IHtmlContent BooleanIcon(bool? boolean) {
            TagBuilder tag = new TagBuilder("span");
            if (boolean.HasValue && boolean.Value) {
                tag.AddCssClass("mdi mdi-check text-success");
            } else if (boolean.HasValue) {
                tag.AddCssClass("mdi mdi-cancel text-danger");
            }
            return tag;
        }

        /// <summary>
        /// Convert the boolean to an icon.
        /// </summary>
        /// <param name="boolean">Boolean to display as an icon</param>
        public static IHtmlContent BooleanIconAndText(bool? boolean) {
            TagBuilder tag = new TagBuilder("span");
            if (boolean.HasValue && boolean.Value) {
                tag.AddCssClass("mdi mdi-checkbox-marked-outline text-success");
                tag.Attributes.Add("title", "True");
            } else if (boolean.HasValue) {
                tag.AddCssClass("mdi mdi-checkbox-blank-off-outline text-danger");
                tag.Attributes.Add("title", "False");
            } else {
                tag.AddCssClass("mdi mdi-checkbox-blank-outline text-secondary");
                tag.Attributes.Add("title", "Unknown");
            }
            TagBuilder container = new TagBuilder("span");
            container.InnerHtml.AppendHtml(tag.ToRawString() + " ");
            if (boolean.HasValue && boolean.Value) {
                container.InnerHtml.AppendHtml("True");
            } else if (boolean.HasValue) {
                container.InnerHtml.AppendHtml("False");
            } else {
                container.InnerHtml.AppendHtml("Unknown");
            }
            return container;
        }

        public static IHtmlContent GetEnumDescription(Enum value) {
            Type type = value.GetType();
            MemberInfo[] member = type.GetMember(value.ToString());

            DescriptionAttribute descriptionAttr = null;
            if (member.SafeAny()) {
                descriptionAttr = member[0].GetCustomAttribute<DescriptionAttribute>(true);
            }

            if (descriptionAttr != null)
                return new HtmlString(descriptionAttr.Description);

            DisplayAttribute displayAttr = null;
            if (member.SafeAny()) {
                displayAttr = member[0].GetCustomAttribute<DisplayAttribute>(true);
            }
            if (displayAttr != null)
                return new HtmlString(displayAttr.Description);

            return new HtmlString(string.Empty);
        }

        /// <summary>
        /// Returns the view of the website project.
        /// </summary>
        public static IHtmlContent GetWebsiteVersion() {
            return new HtmlString(new AssemblyInformation().CallingAssemblyFullVersion.ToString(2));
        }
    }
}
