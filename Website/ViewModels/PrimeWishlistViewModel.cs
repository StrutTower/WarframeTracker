using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TowerSoft.Repository;
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


        public PrimeWishlistViewModel Load(ClaimsPrincipal user, AppSettings appSettings) {
            int userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));

            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                List<WarframeItem> allItems = new WarframeItemUtilities(uow).GetAll();
                List<WarframeItem> primes = allItems.Where(x => x.IsPrime).ToList();

                ItemAcquisitions = new ItemAcquisitionRepository(uow).GetByUserID(userID);

                List<ItemAcquisition> ias = ItemAcquisitions.Where(x => x.IsAcquired).ToList();
                ComponentAcquisitions = new ComponentAcquisitionRepository(uow).GetByUserID(userID);

                PrimeWishlist = primes.Where(x => !x.IsExclusive && !x.IsVaulted && !ias.Select(y => y.ItemUniqueName).Contains(x.UniqueName))
                    .OrderBy(x => x.Name).ToList();

                #region Needed Relics
                NeededRelics = new List<WarframeItem>();
                ItemCategory cat = new ItemCategoryRepository(uow).GetByCodexSection(CodexSection.Relics).FirstOrDefault();
                List<WarframeItem> allRelics = new WarframeItemUtilities(uow).GetByCategoryID(cat.ID);
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
}
