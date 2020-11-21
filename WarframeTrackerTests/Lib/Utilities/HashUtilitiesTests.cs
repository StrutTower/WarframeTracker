using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerTests.Lib.Utilities {
    [TestClass]
    public class HashUtilitiesTests {
        [TestMethod]
        public void Validate_MatchingString_ShouldReturnTrue() {
            string testString = "This is a test string";
            HashUtilities hash = new HashUtilities();
            var saltHash = hash.CreateSaltAndHash(testString);

            bool result = hash.Validate(testString, saltHash.Value, saltHash.Key);

            Assert.IsTrue(result, "The hash and salt did not validate correctly");
        }

        [TestMethod]
        public void Validate_InvalidString_ShouldReturnFalse() {
            string testString = "This is a test string";
            HashUtilities hash = new HashUtilities();
            var saltHash = hash.CreateSaltAndHash(testString);

            bool result = hash.Validate("A different string", saltHash.Value, saltHash.Key);

            Assert.IsFalse(result, "The hash and salt did not validate correctly");
        }
    }
}
