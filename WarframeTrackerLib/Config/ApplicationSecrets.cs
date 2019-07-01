using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace WarframeTrackerLib.Config {
    public class ApplicationSecrets {
        public string ConnectionString { get; set; }

        public string PushbulletApiKey { get; set; }
        
        public string DefaultPushbulletDeviceIden { get; set; }

        public static ApplicationSecrets Get() {
            FileInfo fi = new FileInfo(Assembly.GetEntryAssembly().Location);
            
            string json = File.ReadAllText(Path.Combine(fi.Directory.FullName, "applicationSecrets.json"));
            return JsonConvert.DeserializeObject<ApplicationSecrets>(json);
        }
    }
}
