using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.WarframeApi.WorldState;

namespace WarframeTrackerTests.Lib.WarframeApi.WorldState {
    [TestClass]
    public class VoidTraderTests {
        [TestMethod]
        public void ItemCount_NullList_ShouldReturnZero() {
            VoidTrader voidTrader = new VoidTrader();

            Assert.AreEqual(0, voidTrader.ItemCount);
        }

        [TestMethod]
        public void ItemCount_TwoItems_ShouldReturnTwo() {
            VoidTrader voidTrader = new VoidTrader { Manifest = new List<VoidTraderManifestItem>() };
            voidTrader.Manifest.Add(new VoidTraderManifestItem());
            voidTrader.Manifest.Add(new VoidTraderManifestItem());

            Assert.AreEqual(2, voidTrader.ItemCount);
        }

        [TestMethod]
        public void IsHere_ShouldReturnTrue() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddHours(-3),
                EndDateUtc = DateTime.UtcNow.AddDays(1)
            };

            Assert.IsTrue(voidTrader.IsHere);
        }

        [TestMethod]
        public void IsHere_FutureDates_ShouldReturnFalse() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddHours(3),
                EndDateUtc = DateTime.UtcNow.AddDays(1)
            };

            Assert.IsFalse(voidTrader.IsHere);
        }

        [TestMethod]
        public void IsHere_PastDates_ShouldReturnFalse() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddDays(-1),
                EndDateUtc = DateTime.UtcNow.AddHours(-3)
            };

            Assert.IsFalse(voidTrader.IsHere);
        }

        [TestMethod]
        public void TimeTillDisplay_LessThanHour_ShouldReturnLessThan1H() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddMinutes(10),
                EndDateUtc = DateTime.UtcNow.AddDays(7)
            };

            Assert.AreEqual("<1h", voidTrader.TimeTillDisplay);
        }

        [TestMethod]
        public void TimeTillDisplay_ThreeHours_ShouldReturnLessThan1H() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddHours(3).AddMinutes(1),
                EndDateUtc = DateTime.UtcNow.AddDays(7)
            };

            Assert.AreEqual("3h", voidTrader.TimeTillDisplay);
        }

        [TestMethod]
        public void TimeTillDisplay_OneDayThreeHours_ShouldReturnLessThan1H() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddDays(1).AddHours(3).AddMinutes(1),
                EndDateUtc = DateTime.UtcNow.AddDays(7)
            };

            Assert.AreEqual("1d 3h", voidTrader.TimeTillDisplay);
        }

        [TestMethod]
        public void TimeTillDisplay_TwoDay_ShouldReturnLessThan1H() {
            VoidTrader voidTrader = new VoidTrader {
                StartDateUtc = DateTime.UtcNow.AddDays(2).AddMinutes(1),
                EndDateUtc = DateTime.UtcNow.AddDays(7)
            };

            Assert.AreEqual("2d", voidTrader.TimeTillDisplay);
        }
    }
}
