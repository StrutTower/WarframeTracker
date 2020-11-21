using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WarframeTrackerLib.Utilities {
    public static class JsonHelper {
        public static T ConvertPath<T>(IDictionary<string, JToken> tokenDictionary, string path) {
            string[] parts = path.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (tokenDictionary.ContainsKey(parts[0])) {
                JToken firstToken = tokenDictionary[parts[0]];
                if (parts.Count() > 1) {
                    JToken token = firstToken;
                    foreach(string part in parts.Skip(1)) {
                        if (token.HasValues) {
                            token = token[part];
                        }
                        if (token == null)
                            break;
                    }
                    if (token != null)
                        return token.ToObject<T>();
                } else {
                    return firstToken.ToObject<T>();
                }
            }
            return default;
        }

        public static DateTime MillisecondsToDateTimeUtc(long milliseconds) {
            return GetUtcEpochDateTime().AddMilliseconds(milliseconds);
        }

        public static DateTime GetUtcEpochDateTime() {
            return DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
        }
    }
}
