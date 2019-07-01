using System;

namespace WarframeTrackerLib.Utilities {
    public class JsonDeserializePropertyAttribute : Attribute {
        public JsonDeserializePropertyAttribute(string name) {
            PropertyName = name;
        }

        public string PropertyName { get; set; }
    }
}
