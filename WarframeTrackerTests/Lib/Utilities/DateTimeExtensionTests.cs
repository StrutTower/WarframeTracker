using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerTests.Lib.Utilities {
    [TestClass]
    public class DateTimeExtensionTests {
        [TestMethod]
        public void IsBetween_BewteenDate_ShouldReturnTrue() {
            DateTime testDate = new DateTime(2020, 2, 1);
            DateTime startDate = new DateTime(2020, 1, 1);
            DateTime endDate = new DateTime(2020, 3, 1);

            bool result = testDate.IsBetween(startDate, endDate);

            Assert.IsTrue(result, "The date tested should have been between the other dates");
        }

        [TestMethod]
        public void IsBetween_BeforeDate_ShouldReturnFalse() {
            DateTime testDate = new DateTime(2019, 2, 1);
            DateTime startDate = new DateTime(2020, 1, 1);
            DateTime endDate = new DateTime(2020, 3, 1);

            bool result = testDate.IsBetween(startDate, endDate);

            Assert.IsFalse(result, "The date tested should have been before the other dates");
        }

        [TestMethod]
        public void IsBetween_AfterDate_ShouldReturnFalse() {
            DateTime testDate = new DateTime(2021, 2, 1);
            DateTime startDate = new DateTime(2020, 1, 1);
            DateTime endDate = new DateTime(2020, 3, 1);

            bool result = testDate.IsBetween(startDate, endDate);

            Assert.IsFalse(result, "The date tested should have been after the other dates");
        }
    }
}
