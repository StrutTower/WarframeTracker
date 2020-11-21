using System.Collections.Generic;
using System.Net.Http;
using WarframeTrackerLib.Config;

namespace WarframeTrackerLib.Utilities {
    public class PushBulletHelper {
        public async void SendNotificationToCellPhone(string message, ApplicationSecrets appSecrets) {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Access-Token", appSecrets.PushbulletApiKey);

            Dictionary<string, string> data = new Dictionary<string, string> {
                { "channel_tag", appSecrets.InvasionRewardChannel },
                { "type", "note" },
                { "title", "Warframe Tracker" },
                { "body", message }
            };

            var content = new FormUrlEncodedContent(data);

            var response = await client.PostAsync("https://api.pushbullet.com/v2/pushes", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
