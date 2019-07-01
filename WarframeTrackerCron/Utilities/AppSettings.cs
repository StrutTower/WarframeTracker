using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WarframeTrackerCron.Utilities {
    public class AppSettings {

        public static AppSettings Get() {
            FileInfo fi = new FileInfo(Assembly.GetEntryAssembly().Location);

            string json = File.ReadAllText(Path.Combine(fi.Directory.FullName, "appsettings.json"));
            return JsonConvert.DeserializeObject<AppSettings>(json);
        }
    }
}
