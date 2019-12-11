using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;

namespace Website.ViewModels {
    public class PrimeWishlistViewModel {
        public List<WarframeItem> PrimeWishlist { get; set; }

        public List<ItemAcquisition> ItemAcquisitions { get; set; }

        public List<ComponentAcquisition> ComponentAcquisitions { get; set; }

        public List<WarframeItem> NeededRelics { get; set; }


        public PrimeWishlistViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, ClaimsPrincipal user, AppSettings appSettings) {
            int? userID = null;
            if (user.Identity.IsAuthenticated) {
                userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            List<WarframeItem> allItems = itemUtils.GetAll();
            List<WarframeItem> primes = allItems.Where(x => x.IsPrime).ToList();

            if (userID.HasValue) {
                ItemAcquisitions = uow.GetRepo<ItemAcquisitionRepository>().GetByUserID(userID.Value);
            } else {
                ItemAcquisitions = new List<ItemAcquisition>();
            }

            List<ItemAcquisition> ias = ItemAcquisitions.Where(x => x.IsAcquired).ToList();
            if (userID.HasValue) {
                ComponentAcquisitions = uow.GetRepo<ComponentAcquisitionRepository>().GetByUserID(userID.Value);
            } else {
                ComponentAcquisitions = new List<ComponentAcquisition>();
            }

            List<PrimeRelease> releases = uow.GetRepo<PrimeReleaseRepository>().GetAll().Where(x => x.IsReleasedFromVault).ToList();
            List<string> unvaultedUniqueNames = new List<string>();
            if (releases.Any()) {
                unvaultedUniqueNames.AddRange(releases.Select(x => x.WarframeUniqueName));
                unvaultedUniqueNames.AddRange(releases.Select(x => x.Item1UniqueName));
                unvaultedUniqueNames.AddRange(releases.Select(x => x.Item2UniqueName));
                unvaultedUniqueNames.AddRange(releases.Select(x => x.Item3UniqueName));
            }

            PrimeWishlist = primes.Where(x => !x.IsExclusive && (!x.IsVaulted || unvaultedUniqueNames.Contains(x.UniqueName)) && !ias.Select(y => y.ItemUniqueName).Contains(x.UniqueName))
                .OrderBy(x => x.Name).ToList();

            #region Needed Relics
            NeededRelics = new List<WarframeItem>();
            ItemCategory cat = uow.GetRepo<ItemCategoryRepository>().GetByCodexSection(CodexSection.Relics).FirstOrDefault();
            List<WarframeItem> allRelics = itemUtils.GetByCategoryID(cat.ID);
            foreach (WarframeItem item in PrimeWishlist.Where(x => !x.IsVaulted)) {
                if (item.Components != null) {
                    foreach (Component comp in item.Components) {
                        ComponentAcquisition ca = ComponentAcquisitions
                            .SingleOrDefault(x => x.IsAcquired && x.ItemUniqueName == item.UniqueName && x.ComponentUniqueName == comp.UniqueName);

                        if (ca == null) {
                            if (comp.Drops != null && comp.Drops.Any()) {
                                foreach (Drop drop in comp.Drops) {
                                    var relic = allRelics.SingleOrDefault(x => x.Name == drop.Location);
                                    if (relic != null && !relic.IsVaulted)
                                        NeededRelics.Add(relic);
                                }
                            }
                        }
                    }
                }
            }
            NeededRelics = NeededRelics.Distinct()
                .OrderByDescending(x => x.Name.StartsWith("Lith"))
                .ThenByDescending(x => x.Name.StartsWith("Meso"))
                .ThenByDescending(x => x.Name.StartsWith("Neo"))
                .ThenByDescending(x => x.Name).ToList();
            #endregion

            return this;
        }
    }
}
