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
            AppSettings appSettings = AppSettings.Get();

            WorldState worldState = new WorldStateUtilities().GetWorldState();

            string message = string.Empty;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                uow.BeginTransaction();
                try {
                    SentNotificationRepository sentNotificationRepo = new SentNotificationRepository(uow);
                    InvasionRewardRepository invasionRewardRepo = new InvasionRewardRepository(uow);
                    List<SentNotification> sentNotifications = sentNotificationRepo.GetByType(TypeName);
                    List<string> invastionOIDs = new List<string>();
                    List<InvasionReward> invasionRewardNames = invasionRewardRepo.GetAll();

                    foreach (Invasion invasion in worldState.Invasions) {
                        if (sentNotifications.Any(x => x.DataID == invasion.OID)) continue;

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
                                attackerRewardString = "Attacker Rewards: " + string.Join(", ", rewards);
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
                                defenderRewardString = "Defender Rewards: " + string.Join(", ", rewards);
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(attackerRewardString) || !string.IsNullOrWhiteSpace(defenderRewardString)) {
                            message += "Invasion Rewards: " + Environment.NewLine + "  " + attackerRewardString + Environment.NewLine + "  " + defenderRewardString;
                            invastionOIDs.Add(invasion.OID);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(message)) {
                        foreach (string oid in invastionOIDs) {
                            sentNotificationRepo.Add(new SentNotification {
                                Type = TypeName,
                                DataID = oid
                            });
                        }
                        uow.CommitTransaction();
                    }
                } catch {
                    uow.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
