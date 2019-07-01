using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarframeTrackerLib.Utilities {
    public static class IEnumerableExtensions {
        public static bool SafeAny(this IEnumerable<object> list) {
            if (list == null) return false;
            if (list.Any()) return true;
            return false;
        }
    }
}
