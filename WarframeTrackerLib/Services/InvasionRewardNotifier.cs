using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Config;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi.WorldState;
using WesternStateHospital.WSHUtilities;

namespace WarframeTrackerLib.Services {
    public class InvasionRewardNotifier {
        private readonly UnitOfWork uow;
        private readonly ApplicationSecrets appSecrets;
        private const string TypeName = "InvasionRewards";

        public InvasionRewardNotifier(UnitOfWork uow, IOptions<ApplicationSecrets> appSecrets) {
            this.uow = uow;
            this.appSecrets = appSecrets.Value;
        }

        public void StartTask() {
            WorldStateUtilities worldStateUtilities = new WorldStateUtilities();
            WorldState worldState = worldStateUtilities.GetWorldState();
            var solNodes = worldStateUtilities.GetSolNodeDictionary();

            SentNotificationRepository sentNotificationRepo = uow.GetRepo<SentNotificationRepository>();
            InvasionRewardRepository invasionRewardRepo = uow.GetRepo<InvasionRewardRepository>();

            List<SentNotification> sentNotifications = sentNotificationRepo.GetByType(TypeName);
            List<InvasionReward> invasionRewardNames = invasionRewardRepo.GetAll();

            foreach(Invasion invasion in worldState.Invasions) {
                string message = string.Empty;
                if (invasion.Completed) continue;
                if (sentNotifications.Any(x => x.DataID == invasion.ID)) continue;

                string attackerRewardString = string.Empty;
                string defenderRewardString = string.Empty;

                if (invasion.AttackerRewardItems.SafeAny()) {
                    List<string> rewards = new List<string>();
                    foreach (var reward in invasion.AttackerRewardItems) {
                        InvasionReward rewardInfo = invasionRewardNames.SingleOrDefault(x => x.UniqueName == reward.ItemUniqueName);
                        if (rewardInfo == null) {
                            rewards.Add(reward.ItemUniqueName.Split('/').Last());
                        } else if (rewardInfo.ShowOnHomeView) {
                            rewards.Add(rewardInfo.Name);
                        }
                    }
                    if (rewards.Any()) {
                        attackerRewardString = $"  Attacker: ({ string.Join(", ", rewards)})";
                    }
                }

                if (invasion.DefenderRewardItems.SafeAny()) {
                    List<string> rewards = new List<string>();
                    foreach (var reward in invasion.DefenderRewardItems) {
                        InvasionReward rewardInfo = invasionRewardNames.SingleOrDefault(x => x.UniqueName == reward.ItemUniqueName);
                        if (rewardInfo == null) {
                            rewards.Add(reward.ItemUniqueName.Split('/').Last());
                        } else if (rewardInfo.ShowOnHomeView) {
                            rewards.Add(rewardInfo.Name);
                        }
                    }
                    if (rewards.Any()) {
                        defenderRewardString = $"  Defender: ({string.Join(", ", rewards)})";
                    }
                }


                if (!string.IsNullOrWhiteSpace(attackerRewardString) || !string.IsNullOrWhiteSpace(defenderRewardString)) {
                    invasion.SolNode = solNodes[invasion.Node];

                    message = $"{invasion.SolNode.Name} ";
                    if (invasion.IsBlocked) {
                        message += "(Currently blocked by another invasion) ";
                    }
                    message += $"Invasion Rewards:{Environment.NewLine}";

                    if (!string.IsNullOrWhiteSpace(attackerRewardString)) {
                        message += attackerRewardString;
                    }

                    if (!string.IsNullOrWhiteSpace(attackerRewardString) && !string.IsNullOrWhiteSpace(defenderRewardString)) {
                        message += Environment.NewLine;
                    }

                    if (!string.IsNullOrWhiteSpace(defenderRewardString)) {
                        message += defenderRewardString;
                    }

                    new PushBulletHelper().SendNotificationToCellPhone(message, appSecrets);
                    sentNotificationRepo.Add(new SentNotification {
                        Type = TypeName,
                        DataID = invasion.ID
                    });
                }
            }
        }
    }
}
