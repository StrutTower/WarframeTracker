using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure {
    public class AppSettings {
        public string AppName { get; set; }
        public string DevName { get; set; }
        public string DevHomepage { get; set; }

        public int? JunctionMasteryExperience { get; set; }
        public double? MissionNodeMasteryExperience { get; set; }
    }
}
