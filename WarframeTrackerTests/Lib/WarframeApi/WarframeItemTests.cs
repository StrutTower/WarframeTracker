using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerTests.Lib.WarframeApi {
    [TestClass]
    public class WarframeItemTests {
        [TestMethod]
        public void IsPrime_PrimeItem_ShouldReturnTrue() {
            WarframeItem item = new WarframeItem {
                Name = "Cup Prime"
            };

            Assert.IsTrue(item.IsPrime);
        }

        [TestMethod]
        public void IsPrime_NonPrimeItem_ShouldReturnFalse() {
            WarframeItem item = new WarframeItem {
                Name = "Cup"
            };

            Assert.IsFalse(item.IsPrime);
        }

        [TestMethod]
        public void IsVaulted_NotVaulted_ShouldReturnFalse() {
            WarframeItem item = new WarframeItem {
                Name = "Cup Prime",
                Vaulted = "false"
            };

            Assert.IsFalse(item.IsVaulted);
        }

        [TestMethod]
        public void IsVaulted_Vaulted_ShouldReturnTrue() {
            WarframeItem item = new WarframeItem {
                Name = "Cup Prime",
                Vaulted = "true"
            };

            Assert.IsTrue(item.IsVaulted);
        }

        [TestMethod]
        public void IsVaulted_VaultDateInPast_ShouldReturnTrue() {
            WarframeItem item = new WarframeItem {
                Name = "Cup Prime",
                VaultDate = "2020 01 30"
            };

            Assert.IsTrue(item.IsVaulted);
        }

        [TestMethod]
        public void IsVaulted_VaultDateInFuture_ShouldReturnFalse() {
            WarframeItem item = new WarframeItem {
                Name = "Cup Prime",
                VaultDate = "2400 01 30"
            };

            Assert.IsFalse(item.IsVaulted);
        }
    }
}
