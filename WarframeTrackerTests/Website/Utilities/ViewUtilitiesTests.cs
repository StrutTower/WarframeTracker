using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using WarframeTrackerTests.TestObjects;
using Website.Utilities;

namespace WarframeTrackerTests.Website.Utilities {
    [TestClass]
    public class ViewUtilitiesTests {
        [TestMethod]
        public void BooleanIcon_True_ShouldReturnTrueIcon() {
            var htmlContent = ViewUtilities.BooleanIcon(true);
            var rawHtml = ConvertHtmlContentToString(htmlContent);

            Assert.IsTrue(rawHtml.Contains("mdi-check"));
            Assert.IsTrue(rawHtml.Contains("text-success"));
        }

        [TestMethod]
        public void BooleanIcon_False_ShouldReturnFalseIcon() {
            var htmlContent = ViewUtilities.BooleanIcon(false);
            var rawHtml = ConvertHtmlContentToString(htmlContent);

            Assert.IsTrue(rawHtml.Contains("mdi-cancel"));
            Assert.IsTrue(rawHtml.Contains("text-danger"));
        }

        [TestMethod]
        public void GetEnumDescription_DescriptionAttribute_ShouldReturnDescription() {
            string expected = "Description from DescriptionAttribute";
            var value = TestEnum.WithDescriptionAttribute;
            var actual = ConvertHtmlContentToString(ViewUtilities.GetEnumDescription(value));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEnumDescription_DisplayAttribute_ShouldReturnDescription() {
            string expected = "Description from DisplayAttribute";
            var value = TestEnum.WithDisplayDescription;
            var actual = ConvertHtmlContentToString(ViewUtilities.GetEnumDescription(value));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEnumDescription_BothAttributes_ShouldReturnDescriptionAttributeValue() {
            string expected = "Description from DescriptionAttribute with both";
            var value = TestEnum.WithBoth;
            var actual = ConvertHtmlContentToString(ViewUtilities.GetEnumDescription(value));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEnumDescription_NoAttributes_ShouldReturnNothing() {
            string expected = "";
            var value = TestEnum.None;
            var actual = ConvertHtmlContentToString(ViewUtilities.GetEnumDescription(value));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetWebsiteVersion_ShouldReturnAnything() {
            IHtmlContent htmlContent = ViewUtilities.GetWebsiteVersion();
            string result = ConvertHtmlContentToString(htmlContent);
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.Contains("."));
        }

        private string ConvertHtmlContentToString(IHtmlContent htmlContent) {
            using StringWriter writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
