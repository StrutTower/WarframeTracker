using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeTrackerLib.Attributes;

namespace WarframeTrackerLib.WarframeApi {
    public class DamageTypes {
        [KuvaValence]
        public double? Impact { get; set; }
        public double? Puncture { get; set; }
        public double? Slash { get; set; }
        [KuvaValence]
        public double? Heat { get; set; }
        [KuvaValence]
        public double? Cold { get; set; }
        [KuvaValence]
        public double? Electricity { get; set; }
        [KuvaValence]
        public double? Toxin { get; set; }
        public double? Corrosive { get; set; }
        public double? Blast { get; set; }
        public double? Viral { get; set; }
        [KuvaValence]
        public double? Radiation { get; set; }
        [KuvaValence]
        public double? Magnetic { get; set; }
        public double? Gas { get; set; }
    }
}
