using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WarframeTrackerLib.Utilities {
    public class JsonPathConverter<T> : JsonConverter where T : new() {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var daat = JObject.Load(reader);
            var result = new T();

            foreach (var prop in result.GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)) {
                string propName = string.Empty;
                //filter out non-Json attributes
                var attr = prop.GetCustomAttributes(false).Where(a => a.GetType() == typeof(JsonPropertyAttribute)).FirstOrDefault();
                if (attr != null) {
                    propName = ((JsonPropertyAttribute)attr).PropertyName;
                } else {
                    var attr2 = prop.GetCustomAttribute(typeof(JsonDeserializePropertyAttribute));
                    if (attr2 != null) propName = ((JsonDeserializePropertyAttribute)attr2).PropertyName;
                }
                //If no JsonPropertyAttribute existed, or no PropertyName was set,
                //still attempt to deserialize the class member
                if (string.IsNullOrWhiteSpace(propName)) {
                    propName = prop.Name;
                }
                //split by the delimiter, and traverse recursevly according to the path
                var nests = propName.Split('.');
                object propValue = null;
                JToken token = null;
                for (var i = 0; i < nests.Length; i++) {
                    if (token == null) {
                        token = daat.GetValue(nests[i], StringComparison.InvariantCultureIgnoreCase);
                    } else {
                        token = ((JObject)token).GetValue(nests[i], StringComparison.InvariantCultureIgnoreCase);
                    }
                    if (token == null) {
                        //silent fail: exit the loop if the specified path was not found
                        break;
                    } else {
                        //store the current value
                        if (token is JValue) {
                            propValue = ((JValue)token).Value;
                        }
                    }
                }

                if (propValue != null) {
                    //workaround for numeric values being automatically created as Int64 (long) objects.
                    //if (propValue is long && prop.PropertyType == typeof(Int32)) {
                    //    prop.SetValue(result, Convert.ToInt32(propValue));
                    //} else {
                    //    prop.SetValue(result, propValue);
                    //}
                    prop.SetValue(result, Convert.ChangeType(propValue, prop.PropertyType));
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

        }

        public override bool CanConvert(Type objectType) {
            // CanConvert is not called when [JsonConverter] attribute is used
            return false;
        }

        public override bool CanWrite {
            get { return false; }
        }
    }
}