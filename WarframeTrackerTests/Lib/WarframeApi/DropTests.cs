using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerTests.Lib.WarframeApi {
    [TestClass]
    public class DropTests {
        [TestMethod]
        public void RelicEra_AllEras_ShouldRetunEras() {
            Drop lithDrop = new Drop { Location = "Lith A1 Intact", Type = "Relics" };
            Drop mesoDrop = new Drop { Location = "Meso A1 Intact", Type = "Relics" };
            Drop neoDrop = new Drop { Location = "Neo A1 Intact", Type = "Relics" };
            Drop axiDrop = new Drop { Location = "Axi A1 Intact", Type = "Relics" };

            Assert.AreEqual("Lith", lithDrop.RelicEra);
            Assert.AreEqual("Meso", mesoDrop.RelicEra);
            Assert.AreEqual("Neo", neoDrop.RelicEra);
            Assert.AreEqual("Axi", axiDrop.RelicEra);
        }

        [TestMethod]
        public void RelicType_ValidRelic_ShouldReturnType() {
            Drop relic = new Drop { Location = "Lith A1 Intact", Type = "Relics" };

            Assert.AreEqual("A1", relic.RelicType);
        }

        [TestMethod]
        public void RelicLevel_ValidRelic_ShouldReturnType() {
            Drop relic = new Drop { Location = "Lith A1 Intact", Type = "Relics" };

            Assert.AreEqual("Intact", relic.RelicLevel);
        }
    }
}
