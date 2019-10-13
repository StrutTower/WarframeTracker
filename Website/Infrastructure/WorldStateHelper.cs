using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi.WorldState;

namespace Website.Infrastructure {
    public class WorldStateHelper {
        private WorldStateUtilities _worldStateUtilities;
        private Dictionary<string, string> _factions;
        private Dictionary<string, string> _missionTypes;
        private Dictionary<string, SolNode> _solNodes;

        public WorldStateHelper() {
            _worldStateUtilities = new WorldStateUtilities();
            _factions = _worldStateUtilities.GetFactionDictionary();
            _solNodes = _worldStateUtilities.GetSolNodeDictionary();
            _missionTypes = _worldStateUtilities.GetMissionTypeDictionary();
        }


        public List<Invasion> GetInvasions(UnitOfWork unitOfwork, WorldState worldState) {
            List<Invasion> importantInavsions = new List<Invasion>();
            List<InvasionReward> invasionRewardNames = new InvasionRewardRepository(unitOfwork).GetAll();

            foreach (Invasion invasion in worldState.Invasions.Where(x => !x.Completed)) {
                List<InvasionRewardItem> items = new List<InvasionRewardItem>();
                if (invasion.AttackerRewardItems.SafeAny())
                    items.AddRange(invasion.AttackerRewardItems);
                if (invasion.DefenderRewardItems.SafeAny())
                    items.AddRange(invasion.DefenderRewardItems);

                if (items.Any()) {
                    foreach (InvasionRewardItem item in items) {
                        InvasionReward rewardName = invasionRewardNames.SingleOrDefault(x => x.UniqueName == item.ItemUniqueName);
                        if (rewardName != null)
                            item.Name = rewardName.Name;

                        if (rewardName == null || (rewardName != null && rewardName.ShowOnHomeView)) {
                            invasion.IsImportant = true;
                        }
                    }
                }

                invasion.AttackerFaction = _factions[invasion.AttackerFaction];
                invasion.DefenderFaction = _factions[invasion.DefenderFaction];
                invasion.SolNode = _solNodes[invasion.Node];

                importantInavsions.Add(invasion);
            }
            return importantInavsions;
        }

        public List<ActiveMission> GetVoidAndOrokinCellMissions(UnitOfWork unitOfWork, WorldState worldState) {
            List<ActiveMission> output = new List<ActiveMission>();

            foreach (ActiveMission mission in worldState.ActiveMissions) {
                SolNode node = _solNodes[mission.Node];
                if (node.Name.Contains("Saturn") || node.Name.Contains("Ceres")) {
                    mission.SolNode = node;
                    mission.MissionType = _missionTypes[mission.MissionType];
                    output.Add(mission);
                }
            }

            return output;
        }
    }
}
