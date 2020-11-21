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

            List<PrimeRelease> releases = uow.GetRepo<PrimeReleaseRepository>().GetAll();
            List<PrimeRelease> unvaulted = releases.Where(x => x.IsReleasedFromVault).ToList();
            List<string> unvaultedUniqueNames = new List<string>();
            if (unvaulted.Any()) {
                unvaultedUniqueNames.AddRange(unvaulted.Select(x => x.WarframeUniqueName));
                unvaultedUniqueNames.AddRange(unvaulted.Select(x => x.Item1UniqueName));
                unvaultedUniqueNames.AddRange(unvaulted.Select(x => x.Item2UniqueName));
                unvaultedUniqueNames.AddRange(unvaulted.Select(x => x.Item3UniqueName));
            }

            PrimeWishlist = primes.Where(x => !x.IsExclusive && (!x.IsVaulted || unvaultedUniqueNames.Contains(x.UniqueName)) && !ias.Select(y => y.ItemUniqueName).Contains(x.UniqueName)).ToList();

            foreach (WarframeItem item in PrimeWishlist) {
                if (string.IsNullOrWhiteSpace(item.ReleaseDate)) {
                    PrimeRelease primeRelease = releases.SingleOrDefault(x => x.WarframeUniqueName == item.UniqueName || x.Item1UniqueName == item.UniqueName || x.Item2UniqueName == item.UniqueName || x.Item3UniqueName == item.UniqueName);
                    if (primeRelease != null)
                        item.ReleaseDate = primeRelease.ReleaseDate.ToString("yyyy MM dd");
                }
                if (item.Components != null)
                    item.Components = item.Components.OrderByDescending(x => x.Name == "Blueprint").ThenBy(x => x.Name == "Orokin Cell").ThenBy(x => x.Name).ToList();
            }

            PrimeWishlist = PrimeWishlist.OrderBy(x => x.ReleaseDate).ToList();

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
                                    string[] split = drop.Location.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                                    if (split.Length > 1) {
                                        string relicName = split[0] + " " + split[1];
                                        var relic = allRelics.SingleOrDefault(x => x.Name.StartsWith(relicName + " "));
                                        //var relic = allRelics.Where(x => x.Name.Substring(0, 8) == drop.Location.Substring(0, 8));
                                        if (relic != null && !relic.IsVaulted)
                                            NeededRelics.Add(relic);
                                    }
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
