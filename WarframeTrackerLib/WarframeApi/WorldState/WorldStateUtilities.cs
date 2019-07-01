using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class WorldStateUtilities {
        readonly string _apiUrl = "http://content.warframe.com/dynamic/worldState.php";

        readonly string _solNodeJson = "https://raw.githubusercontent.com/WFCD/warframe-worldstate-data/master/data/solNodes.json";

        readonly string _factionsJson = "https://raw.githubusercontent.com/WFCD/warframe-worldstate-data/master/data/factionsData.json";

        readonly string _missionTypeJson = "https://raw.githubusercontent.com/WFCD/warframe-worldstate-data/master/data/missionTypes.json";

        public WorldState GetWorldState() {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (WebClient client = new WebClient { UseDefaultCredentials = true }) {

                string json = client.DownloadString(_apiUrl);
                return JsonConvert.DeserializeObject<WorldState>(json);
            }
        }

        public Dictionary<string, SolNode> GetSolNodeDictionary() {
            Dictionary<string, SolNode> solNodeDictionary = new Dictionary<string, SolNode>();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (WebClient client = new WebClient { UseDefaultCredentials = true }) {

                string solNodeJson = client.DownloadString(_solNodeJson);
                JObject solNodeObject = JsonConvert.DeserializeObject<JObject>(solNodeJson);

                foreach (JProperty prop in solNodeObject.Properties()) {
                    SolNode solNode = new SolNode {
                        Key = prop.Name
                    };
                    if (solNodeObject[prop.Name]["value"] != null)
                        solNode.Name = solNodeObject[prop.Name]["value"].ToString();

                    if (solNodeObject[prop.Name]["enemy"] != null)
                        solNode.Faction = solNodeObject[prop.Name]["enemy"].ToString();

                    if (solNodeObject[prop.Name]["type"] != null)
                        solNode.Type = solNodeObject[prop.Name]["type"].ToString();

                    solNodeDictionary.Add(solNode.Key, solNode);
                }

            }
            return solNodeDictionary;
        }

        public Dictionary<string, string> GetFactionDictionary() {
            Dictionary<string, string> factionDictionary = new Dictionary<string, string>();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (WebClient client = new WebClient { UseDefaultCredentials = true }) {
                string factionJson = client.DownloadString(_factionsJson);
                JObject factionObject = JsonConvert.DeserializeObject<JObject>(factionJson);

                foreach (JProperty prop in factionObject.Properties()) {
                    factionDictionary.Add(prop.Name, factionObject[prop.Name]["value"].ToString());
                }
            }
            return factionDictionary;
        }

        public Dictionary<string, string> GetMissionTypeDictionary() {
            Dictionary<string, string> missionTypeDictionary = new Dictionary<string, string>();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (WebClient client = new WebClient { UseDefaultCredentials = true }) {
                string json = client.DownloadString(_missionTypeJson);
                JObject jObject = JsonConvert.DeserializeObject<JObject>(json);

                foreach (JProperty prop in jObject.Properties()) {
                    missionTypeDictionary.Add(prop.Name, jObject[prop.Name]["value"].ToString());
                }
            }
            return missionTypeDictionary;
        }
    }
}
