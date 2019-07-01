using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeTrackerLib.WarframeApi {
    public class Component {
        public string UniqueName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ItemCount { get; set; }
        public string ImageName { get; set; }

        public List<Drop> Drops { get; set; }
    }
}
