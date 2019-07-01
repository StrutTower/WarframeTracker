using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using WarframeTrackerLib.WarframeApi.WorldState;
using Website.Infrastructure;

namespace Website.ViewModels {
    public class HomeViewModel {
        public WorldState WorldState { get; set; }

        public MasteryRank MasteryRank { get; set; }

        public int CurrentMasteryExperience { get; set; }

        public List<Invasion> Invasions { get; set; }

        public List<ActiveMission> VoidMissions { get; set; }

        public HomeViewModel Load(ClaimsPrincipal user, AppSettings appSettings) {
            int userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            WorldState = new WorldStateUtilities().GetWorldState();

            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                List<ItemAcquisition> ias = new ItemAcquisitionRepository(uow).GetByUserID(userID);

                #region Mastery
                int masteryExp = 0;
                List<ItemCategory> ics = new ItemCategoryRepository(uow).GetAll();
                List<WarframeItem> allItems = new WarframeItemUtilities(uow).GetAll();

                foreach (ItemAcquisition ia in ias) {
                    if (ia.IsMastered) {
                        WarframeItem item = allItems.SingleOrDefault(x => x.UniqueName == ia.ItemUniqueName);
                        if (item == null) {
                            string name = ia.ItemUniqueName;//TODO add check for missing UniqueName
                            Console.WriteLine();
                        }
                        ItemCategory ic = ics.SingleOrDefault(x => x.ID == item.ItemCategoryID);
                        if (ic.CanBeMastered && ic.MaxRankExperience.HasValue)
                            masteryExp += ic.MaxRankExperience.Value;
                    }
                }

                UserData userData = new UserDataRepository(uow).GetByUserID(userID);
                if (userData != null) {
                    if (appSettings.JunctionMasteryExperience.HasValue) {
                        int junctionExp = userData.JunctionsCompleted * appSettings.JunctionMasteryExperience.Value;
                        masteryExp += junctionExp;
                    }
                    if (appSettings.MissionNodeMasteryExperience.HasValue) {
                        double nodeExp = (double)userData.MissionNodesCompleted * appSettings.MissionNodeMasteryExperience.Value;
                        masteryExp += (int)Math.Ceiling(nodeExp);
                    }
                }

                List<MasteryRank> ranks = new MasteryRankRepository(uow).GetAll();
                ranks = ranks.OrderBy(x => x.Number).ToList();

                MasteryRank current = null;
                foreach (MasteryRank rank in ranks) {
                    if (rank.ExperienceRequired < masteryExp)
                        current = rank;
                    if (rank.ExperienceRequired > masteryExp) {
                        current.NextRankExperience_ = rank.ExperienceRequired;
                        break;
                    }
                }
                MasteryRank = current;
                CurrentMasteryExperience = masteryExp;
                #endregion

                WorldStateHelper worldStateHelper = new WorldStateHelper();

                Invasions = worldStateHelper.GetImportantInvasion(uow, WorldState);

                VoidMissions = worldStateHelper.GetVoidAndOrokinCellMissions(uow, WorldState);
            }
            return this;
        }
    }
}
