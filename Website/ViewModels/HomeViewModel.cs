using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi.WorldState;
using Website.Infrastructure;

namespace Website.ViewModels {
    public class HomeViewModel {
        public WorldState WorldState { get; set; }

        public List<Invasion> ImportantInvasions { get; set; }

        public List<Invasion> OtherInvasions { get; set; }

        public List<ActiveMission> VoidMissions { get; set; }

        public HomeViewModel Load(ClaimsPrincipal user, AppSettings appSettings) {
            //int userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            WorldState = new WorldStateUtilities().GetWorldState();

            using (UnitOfWork uow = UnitOfWork.CreateNew()) {

                WorldStateHelper worldStateHelper = new WorldStateHelper();

                List<Invasion> invasions = worldStateHelper.GetInvasions(uow, WorldState);
                ImportantInvasions = invasions.Where(x => x.IsImportant).ToList();
                OtherInvasions = invasions.Where(x => !x.IsImportant).ToList();

                VoidMissions = worldStateHelper.GetVoidAndOrokinCellMissions(uow, WorldState);
            }
            return this;
        }
    }
}
