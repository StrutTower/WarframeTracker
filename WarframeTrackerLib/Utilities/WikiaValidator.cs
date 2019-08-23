using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerLib.Utilities {
    public class WikiaValidator {
        private string WarframeApiUrl = "https://wf.snekw.com/warframes-wiki";
        private string WeaponApiUrl = "https://wf.snekw.com/weapons-wiki";

        public void ValidateItems() {
            var test2 = GetWarframeNames();
            var test = GetWeaponNames();


            Console.WriteLine();
        }

        private List<string> GetWarframeNames() {
            List<string> names = new List<string>();
            JToken token = GetApiJsonData(WarframeApiUrl);

            // Ignore list
            JToken[] ignoreList = token.SelectToken("data.IgnoreInCount").ToArray();
            List<string> ignores = new List<string>();
            foreach (var prop in ignoreList) {
                ignores.Add(prop.ToString());
            }
            JToken list = token.SelectToken("data.Warframes");
            if (list != null) {
                foreach (JProperty item in list.ToArray()) {
                    if (!ignores.Contains(item.Name))
                        names.Add(item.Name);
                }
            }
            return names;
        }

        private List<string> GetWeaponNames() {
            List<string> names = new List<string>();
            JToken token = GetApiJsonData(WeaponApiUrl);

            // ignore list
            JToken[] ignoreList = token.SelectToken("data.IgnoreInCount").ToArray();
            List<string> ignores = new List<string>();
            foreach (var prop in ignoreList) {
                ignores.Add(prop.ToString());
            }
            JToken list = token.SelectToken("data.Weapons");
            if (list != null) {
                foreach (JProperty item in list.ToArray()) {
                    if (!ignores.Contains(item.Name))
                        names.Add(item.Name);
                }
            }
            return names;
        }

        private JToken GetApiJsonData(string url) {
            string response;
            using (WebClient client = new WebClient { UseDefaultCredentials = true }) {
                response = client.DownloadString(url);
            }

            return JsonConvert.DeserializeObject<JToken>(response);
        }
    }
}
