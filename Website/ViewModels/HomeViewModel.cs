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

        public VoidTrader Baro { get; set; }

        public List<Invasion> ImportantInvasions { get; set; }

        public List<Invasion> OtherInvasions { get; set; }

        public List<ActiveMission> CellFissureMissions { get; set; }

        public List<ActiveMission> ExcavationFissureMissions { get; set; }

        public HomeViewModel Load(UnitOfWork uow, ClaimsPrincipal user, AppSettings appSettings) {
            WorldState = new WorldStateUtilities().GetWorldState();

            WorldStateHelper worldStateHelper = new WorldStateHelper();

           Baro = worldStateHelper.GetBaro(WorldState);

            List<Invasion> invasions = worldStateHelper.GetInvasions(uow, WorldState);
            ImportantInvasions = invasions.Where(x => x.IsImportant).ToList();
            OtherInvasions = invasions.Where(x => !x.IsImportant).ToList();

            var voidMissions = worldStateHelper.GetVoidMissions(WorldState);

            CellFissureMissions = voidMissions.Where(x => x.SolNode.Name.Contains("Saturn") || x.SolNode.Name.Contains("Ceres")).ToList();
            ExcavationFissureMissions = voidMissions.Where(x => x.MissionType == "Excavation").ToList();

            return this;
        }
    }
}
