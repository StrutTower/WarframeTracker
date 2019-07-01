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
    public class ItemModalViewModel {
        public WarframeItem WarframeItem { get; set; }

        public ItemCategory ItemCategory { get; set; }

        public ItemAcquisition ItemAcquisition { get; set; }

        public List<ComponentAcquisition> ComponentAcquisitions { get; set; }

        public ItemModalViewModel Load(WarframeItem warframeItem, ItemCategory itemCategory, ClaimsPrincipal user, IUnitOfWork uow) {
            WarframeItem = warframeItem;
            ItemCategory = itemCategory;

            int userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));

            ItemAcquisition = new ItemAcquisitionRepository(uow).GetByPrimaryKeys(userID, WarframeItem.UniqueName);

            if (ItemAcquisition == null)
                ItemAcquisition = new ItemAcquisition {
                    UserID = userID,
                    ItemUniqueName = WarframeItem.UniqueName
                };

            if (WarframeItem.Components != null && WarframeItem.Components.Any()) {
                WarframeItem.Components = WarframeItem.Components.OrderBy(x => x.Name).ToList();

                ComponentAcquisitions = new ComponentAcquisitionRepository(uow)
                    .GetByItemUniqueName(WarframeItem.UniqueName);

                int index = 0;
                foreach (Component comp in WarframeItem.Components) {
                    ComponentAcquisition ca = ComponentAcquisitions
                        .SingleOrDefault(x => x.ComponentUniqueName == comp.UniqueName);
                    if (ca == null) {
                        ComponentAcquisitions.Add(new ComponentAcquisition {
                            UserID = userID,
                            ComponentUniqueName = comp.UniqueName,
                            ItemUniqueName = WarframeItem.UniqueName,
                            ComponentName = comp.Name
                        });
                    } else {
                        ca.ComponentName = comp.Name;
                    }
                    index++;
                }

                ComponentAcquisitions = ComponentAcquisitions.OrderBy(x => x.ComponentName).ToList();
            }
            return this;
        }
    }
}
