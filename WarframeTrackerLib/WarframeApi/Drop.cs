using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeTrackerLib.WarframeApi {
    public class Drop {
        public string Location { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public double? Chance { get; set; }
        public string Rotation { get; set; }

        public string RelicEra {
            get {
                if (Type == "Relics") {
                    if (Location.StartsWith("Lith"))
                        return "Lith";
                    else if (Location.StartsWith("Meso"))
                        return "Meso";
                    else if (Location.StartsWith("Neo"))
                        return "Neo";
                    else if (Location.StartsWith("Axi"))
                        return "Axi";
                }
                return null;
            }
        }

        public string RelicType {
            get {
                if (Type == "Relics") {
                    string[] parts = Location.Split(' ');
                    return parts[1];
                }
                return null;
            }
        }

        public string RelicLevel {
            get {
                if (Type == "Relics") {
                    string[] parts = Location.Split(' ');
                    return parts[2];
                }
                return null;
            }
        }
    }
}
