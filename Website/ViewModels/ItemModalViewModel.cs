﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class ItemModalViewModel {
        public WarframeItem WarframeItem { get; set; }

        public ItemCategory ItemCategory { get; set; }

        public ItemAcquisition ItemAcquisition { get; set; }

        public List<ComponentAcquisition> ComponentAcquisitions { get; set; }


        public ItemModalViewModel Load(WarframeItem warframeItem, ItemCategory itemCategory, ClaimsPrincipal user, UnitOfWork uow, WarframeItemUtilities itemUtils) {
            WarframeItem = warframeItem;
            ItemCategory = itemCategory;

            int? userID = null;
            if (user.Identity.IsAuthenticated) {
                userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                ItemAcquisition = uow.GetRepo<ItemAcquisitionRepository>().GetByPrimaryKeys(userID.Value, WarframeItem.UniqueName);

                if (ItemAcquisition == null)
                    ItemAcquisition = new ItemAcquisition {
                        UserID = userID.Value,
                        ItemUniqueName = WarframeItem.UniqueName
                    };
            }

            if (WarframeItem.Components != null && WarframeItem.Components.Any()) {
                WarframeItem.Components = WarframeItem.Components.OrderByDescending(x => x.Name == "Blueprint").ThenBy(x => x.Name == "Orokin Cell").ThenBy(x => x.Name).ToList();

                if (userID.HasValue) {
                    ComponentAcquisitions = uow.GetRepo<ComponentAcquisitionRepository>()
                        .GetByItemUniqueNameAndUserID(WarframeItem.UniqueName, userID.Value);
                }else {
                    ComponentAcquisitions = new List<ComponentAcquisition>();
                }

                ItemCategory cat = uow.GetRepo<ItemCategoryRepository>().GetByCodexSection(CodexSection.Relics).FirstOrDefault();
                List<WarframeItem> relics = itemUtils.GetByCategoryID(cat.ID);


                int index = 0;
                foreach (Component comp in WarframeItem.Components) {
                    ComponentAcquisition ca = ComponentAcquisitions
                        .SingleOrDefault(x => x.ComponentUniqueName == comp.UniqueName);
                    if (ca == null) {
                        ComponentAcquisitions.Add(new ComponentAcquisition {
                            UserID = userID ?? 0,
                            ComponentUniqueName = comp.UniqueName,
                            ItemUniqueName = WarframeItem.UniqueName,
                            ComponentName = comp.Name
                        });
                    } else {
                        ca.ComponentName = comp.Name;
                    }

                    if (comp.Drops != null && comp.Drops.Any()) {
                        for (int i = comp.Drops.Count - 1; i >= 0; i--) {
                            var relic = relics.SingleOrDefault(x => x.Name == comp.Drops[i].Location);
                            //if (relic != null && relic.IsVaulted) {
                            //    comp.Drops.RemoveAt(i);
                            //}
                        }
                    }
                    index++;
                }

                ComponentAcquisitions = ComponentAcquisitions.OrderByDescending(x => x.ComponentName == "Blueprint").ThenBy(x => x.ComponentName == "Orokin Cell").ThenBy(x => x.ComponentName).ToList();
            }
            return this;
        }
    }
}
