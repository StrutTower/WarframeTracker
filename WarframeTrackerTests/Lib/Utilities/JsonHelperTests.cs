using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using WarframeTrackerLib.Utilities;
using WarframeTrackerTests.TestObjects;

namespace WarframeTrackerTests.Lib.Utilities {
    [TestClass]
    public class JsonHelperTests {
        [TestMethod]
        public void ConvertPath_SingleLevelSimpleObject_ShouldConvert() {
            int expectedInt = 10;
            string expectedString = "Test String";
            bool expectedBool = true;
            var dictionary = new Dictionary<string, JToken> {
                {"int", new JValue(expectedInt) },
                {"string", new JValue(expectedString) },
                { "bool", new JValue(expectedBool) }
            };
            int actualInt = JsonHelper.ConvertPath<int>(dictionary, "int");
            string actualString = JsonHelper.ConvertPath<string>(dictionary, "string");
            bool actualBool = JsonHelper.ConvertPath<bool>(dictionary, "bool");
            Assert.AreEqual(expectedInt, actualInt);
            Assert.AreEqual(expectedString, actualString);
            Assert.AreEqual(expectedBool, actualBool);
        }

        [TestMethod]
        public void ConvertPath_NestedSimpleObject_ShouldConvert() {
            int expectedInt = 10;
            string expectedString = "Test String";
            bool expectedBool = true;
            var dictionary = new Dictionary<string, JToken> {
                {"int", new JObject()["value"] = expectedInt },
                {"string", new JObject()["value"] = expectedString },
                {"bool", new JObject()["value"] = expectedBool }
            };
            int actualInt = JsonHelper.ConvertPath<int>(dictionary, "int.value");
            string actualString = JsonHelper.ConvertPath<string>(dictionary, "string.value");
            bool actualBool = JsonHelper.ConvertPath<bool>(dictionary, "bool.value");
            Assert.AreEqual(expectedInt, actualInt);
            Assert.AreEqual(expectedString, actualString);
            Assert.AreEqual(expectedBool, actualBool);
        }

        [TestMethod]
        public void ConvertPath_SingleLevelCustomObject_ShouldConvert() {
            TestObject expected = new TestObject {
                ID = 5,
                Name = "Test Object",
                Timestamp = DateTime.UtcNow
            };
            var dictionary = new Dictionary<string, JToken> {
                { "object", JToken.FromObject(expected) }
            };
            TestObject actual = JsonHelper.ConvertPath<TestObject>(dictionary, "object");
            Assert.IsTrue(expected.Equals(actual));
        }

        [TestMethod]
        public void ConvertPath_NestedCustomObject_ShouldConvert() {
            TestObject expected = new TestObject {
                ID = 5,
                Name = "Test Object",
                Timestamp = DateTime.UtcNow
            };
            object obj = new { value = expected };
            var dictionary = new Dictionary<string, JToken> {
                { "object", JObject.FromObject(obj) }
            };
            TestObject actual = JsonHelper.ConvertPath<TestObject>(dictionary, "object.value");
            Assert.IsTrue(expected.Equals(actual));
        }

        [TestMethod]
        public void MillisecondsToDateTimeUtc_ShouldReturnValidDateTime() {
            DateTime expected = DateTime.UtcNow;
            TimeSpan diff = expected.Subtract(JsonHelper.GetUtcEpochDateTime());
            long milliseconds = (long)Math.Round(diff.TotalMilliseconds);

            DateTime actual = JsonHelper.MillisecondsToDateTimeUtc(milliseconds);

            // Compare specific values since the dates are slightly 
            // different at values less then milliseconds
            Assert.AreEqual(expected.Year, actual.Year);
            Assert.AreEqual(expected.Month, actual.Month);
            Assert.AreEqual(expected.Day, actual.Day);
            Assert.AreEqual(expected.Hour, actual.Hour);
            Assert.AreEqual(expected.Minute, actual.Minute);
            Assert.AreEqual(expected.Second, actual.Second);

            // Rounding above seems to cause the milliseconds to be slightly off
            int millisecondsDifference = Math.Abs(actual.Millisecond - expected.Millisecond);
            Assert.IsTrue(millisecondsDifference < 2);
        }

        [TestMethod]
        public void GetUtcEpochDateTime_ShouldReturnDate() {
            DateTime expected = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            DateTime actual = JsonHelper.GetUtcEpochDateTime();
            Assert.AreEqual(expected, actual);
        }
    }
}
