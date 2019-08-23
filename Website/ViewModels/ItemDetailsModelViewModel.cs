using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class ItemDetailsModelViewModel {
        public WarframeItem WarframeItem { get; set; }

        public List<WarframeItem> Relics { get; set; }

        public List<ComponentAcquisition> ComponentAcquisitions { get; set; }

        public ItemDetailsModelViewModel Load(WarframeItem item, ClaimsPrincipal user, IUnitOfWork uow) {
            WarframeItem = item;

            int userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));

            ComponentAcquisitions = new ComponentAcquisitionRepository(uow).GetByItemUniqueNameAndUserID(item.UniqueName, userID);

            if (WarframeItem.Components != null && WarframeItem.Components.Any()) {

                ItemCategory cat = new ItemCategoryRepository(uow).GetByCodexSection(CodexSection.Relics).FirstOrDefault();
                Relics = new WarframeItemUtilities(uow).GetByCategoryID(cat.ID);

                foreach (Component comp in WarframeItem.Components) {
                    if (comp.Drops != null && comp.Drops.Any()) {
                        for (int i = comp.Drops.Count - 1; i >= 0; i--) {
                            var relic = Relics.SingleOrDefault(x => x.Name == comp.Drops[i].Location);
                            if (relic != null && relic.IsVaulted) {
                                comp.Drops.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            return this;
        }
    }
}
