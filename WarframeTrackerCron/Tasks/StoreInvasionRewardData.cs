using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi.WorldState;

namespace WarframeTrackerCron.Tasks {
    public class StoreInvasionRewardData : IAppTask {
        public void StartTask() {
            WorldState worldState = new WorldStateUtilities().GetWorldState();
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                uow.BeginTransaction();
                try {
                    TrackedDataRepository tdRepo = new TrackedDataRepository(uow);

                    if (worldState.Invasions != null && worldState.Invasions.Any()) {
                        foreach (Invasion invasion in worldState.Invasions) {

                            var existing = tdRepo.GetByDataID(invasion.OID);
                            if (existing.Any())
                                continue;

                            if (invasion.AttackerRewardItems.SafeAny() || invasion.DefenderRewardItems.SafeAny()) {
                                List<TrackedData> datas = new List<TrackedData>();
                                if (invasion.AttackerRewardItems.SafeAny()) {
                                    foreach (var aReward in invasion.AttackerRewardItems) {
                                        datas.Add(new TrackedData {
                                            Type = "InvasionRewards",
                                            DataID = invasion.OID,
                                            Data = aReward.ItemUniqueName
                                        });
                                    }
                                }
                                if (invasion.DefenderRewardItems.SafeAny()) {
                                    foreach (var dReward in invasion.DefenderRewardItems) {
                                        datas.Add(new TrackedData {
                                            Type = "InvasionRewards",
                                            DataID = invasion.OID,
                                            Data = dReward.ItemUniqueName
                                        });
                                    }
                                }

                                if (datas.Any()) {
                                    foreach (var data in datas) {
                                        tdRepo.Add(data);
                                    }
                                }
                            }
                        }
                    }
                    uow.CommitTransaction();
                } catch {
                    uow.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
