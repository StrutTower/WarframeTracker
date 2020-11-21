using System;
using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;

namespace Website.Areas.Admin.ViewModels {
    public class AdminIndexViewModel {
        public List<string> WarframesMissingReleaseDate { get; set; } = new List<string>();

        public List<string> InvasionRewardsMissingNames { get; set; } = new List<string>();

        public AdminIndexViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtilities, AppSettings appSettings) {
            List<WarframeItem> warframes = itemUtilities.GetByCategoryID(appSettings.WarframeItemCategoryID);

            // Warframe Release Dates
            foreach (WarframeItem warframe in warframes.Where(x => string.IsNullOrWhiteSpace(x.ReleaseDate))) {
                if (!DateTime.TryParse(warframe.ReleaseDate, out DateTime _)) {
                    WarframesMissingReleaseDate.Add(warframe.Name);
                }
            }

            // Invasion Reward Names
            List<string> trackedData = uow.GetRepo<TrackedDataRepository>().GetGroupedByType("InvasionRewards").Select(x => x.Data).ToList();
            List<string> existingInvasionRewards = uow.GetRepo<InvasionRewardRepository>().GetAll().Select(x => x.UniqueName).ToList();
            foreach (string data in trackedData) {
                if (!existingInvasionRewards.Any(x => x.Equals(data))) {
                    InvasionRewardsMissingNames.Add(data);
                }
            }


            return this;
        }
    }
}
