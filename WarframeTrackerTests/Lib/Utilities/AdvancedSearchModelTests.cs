using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerTests.Lib.Utilities {
    [TestClass]
    public class AdvancedSearchModelTests {
        [TestMethod]
        public void GetMasteryRequirementParts_Commas_ShouldReturnCommaList() {
            List<string> expected = new List<string> { "1", "2", "3", "4", "5", "6" };
            AdvancedSearchModel model = new AdvancedSearchModel {
                MasteryRequirement = "1,2,3, 4,5,6"
            };

            List<string> actual = model.GetMasteryRequirementParts();

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual));
        }

        [TestMethod]
        public void GetMasteryRequirementParts_Range_ShouldReturnCommaList() {
            List<string> expected = new List<string> { "1-3", "6-9" };
            AdvancedSearchModel model = new AdvancedSearchModel {
                MasteryRequirement = "1-3, 6- 9"
            };

            List<string> actual = model.GetMasteryRequirementParts();

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual));
        }

        [TestMethod]
        public void GetMasteryRequirementParts_CommaAndRange_ShouldReturnCommaList() {
            List<string> expected = new List<string> { "1", "2", "3", "6-9" };
            AdvancedSearchModel model = new AdvancedSearchModel {
                MasteryRequirement = "1,2, 3, 6- 9"
            };

            List<string> actual = model.GetMasteryRequirementParts();

            Assert.AreEqual(string.Join(",", expected), string.Join(",", actual));
        }

        [TestMethod]
        public void Validate_Valid_ShouldBeValid() {
            AdvancedSearchModel model = new AdvancedSearchModel {
                MasteryRequirement = "1,2,3,6-9"
            };

            var validationResults = model.Validate(null);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        public void Validate_Valid_ShouldBeInvalid() {
            AdvancedSearchModel model = new AdvancedSearchModel {
                MasteryRequirement = "1f,2,x3,6-9,"
            };

            var validationResults = model.Validate(null);
            Assert.IsTrue(validationResults.Any());
        }

        [TestMethod]
        public void Validate_NoSearchCriteria_ShouldBeInvalid() {
            AdvancedSearchModel model = new AdvancedSearchModel();

            var validationResults = model.Validate(null);
            Assert.IsTrue(validationResults.Any());
        }
    }
}
