using System;
using System.Collections.Generic;
using System.Linq;
using TowerSoft.Repository;
using WarframeTrackerCron.Utilities;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi.WorldState;

namespace WarframeTrackerCron.Tasks {
    public class InvasionRewardNotification : IAppTask {
        private const string TypeName = "InvasionRewards";

        public void StartTask() {
            //AppSettings appSettings = AppSettings.Get();

            //WorldStateUtilities worldStateUtilities = new WorldStateUtilities();
            //WorldState worldState = worldStateUtilities.GetWorldState();
            //var solNodes = worldStateUtilities.GetSolNodeDictionary();

            //using (UnitOfWork uow = UnitOfWork.CreateNew()) {
            //    SentNotificationRepository sentNotificationRepo = new SentNotificationRepository(uow);
            //    InvasionRewardRepository invasionRewardRepo = new InvasionRewardRepository(uow);
            //    List<SentNotification> sentNotifications = sentNotificationRepo.GetByType(TypeName);
            //    List<InvasionReward> invasionRewardNames = invasionRewardRepo.GetAll();

            //    foreach (Invasion invasion in worldState.Invasions) {
            //        string message = string.Empty;
            //        if (invasion.Completed) continue;
            //        if (sentNotifications.Any(x => x.DataID == invasion.OID)) continue;

            //        string attackerRewardString = string.Empty;
            //        string defenderRewardString = string.Empty;

            //        if (invasion.AttackerRewardItems.SafeAny()) {
            //            List<string> rewards = new List<string>();
            //            foreach (var reward in invasion.AttackerRewardItems) {
            //                InvasionReward rewardInfo = invasionRewardNames.SingleOrDefault(x => x.UniqueName == reward.ItemUniqueName);
            //                if (rewardInfo == null) {
            //                    rewards.Add(reward.ItemUniqueName.Split('/').Last());
            //                } else if (rewardInfo.ShowOnHomeView) {
            //                    rewards.Add(rewardInfo.Name);
            //                }
            //            }
            //            if (rewards.Any()) {
            //                attackerRewardString = $"  Attacker: ({ string.Join(", ", rewards)})";
            //            }
            //        }

            //        if (invasion.DefenderRewardItems.SafeAny()) {
            //            List<string> rewards = new List<string>();
            //            foreach (var reward in invasion.DefenderRewardItems) {
            //                InvasionReward rewardInfo = invasionRewardNames.SingleOrDefault(x => x.UniqueName == reward.ItemUniqueName);
            //                if (rewardInfo == null) {
            //                    rewards.Add(reward.ItemUniqueName.Split('/').Last());
            //                } else if (rewardInfo.ShowOnHomeView) {
            //                    rewards.Add(rewardInfo.Name);
            //                }
            //            }
            //            if (rewards.Any()) {
            //                defenderRewardString = $"  Defender: ({string.Join(", ", rewards)})";
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(attackerRewardString) || !string.IsNullOrWhiteSpace(defenderRewardString)) {
            //            invasion.SolNode = solNodes[invasion.Node];

            //            message = $"{invasion.SolNode.Name} Invasion Rewards:{Environment.NewLine}";

            //            if (!string.IsNullOrWhiteSpace(attackerRewardString)) {
            //                message += attackerRewardString;
            //            }

            //            if (!string.IsNullOrWhiteSpace(attackerRewardString) && !string.IsNullOrWhiteSpace(defenderRewardString)) {
            //                message += Environment.NewLine;
            //            }

            //            if(!string.IsNullOrWhiteSpace(defenderRewardString)) {
            //                message += defenderRewardString;
            //            }

            //            new PushBulletHelper().SendNotificationToCellPhone(message);
            //            sentNotificationRepo.Add(new SentNotification {
            //                Type = TypeName,
            //                DataID = invasion.OID
            //            });
            //        }
            //    }
            //}
        }
    }
}