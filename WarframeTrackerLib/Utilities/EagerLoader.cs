﻿using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerLib.Utilities {
    public class EagerLoader {
        UnitOfWork _uow;

        public EagerLoader(UnitOfWork uow) {
            _uow = uow;
        }

        public void Load(IEnumerable<CodexTab> codexTabs) {
            foreach (var tab in codexTabs)
                tab.ItemCategory_Objects = _uow.GetRepo<ItemCategoryRepository>().GetByCodexTabID(tab.ID);
        }

        public void Load(CodexTab codexTab) {
            codexTab.ItemCategory_Objects = _uow.GetRepo<ItemCategoryRepository>().GetByCodexTabID(codexTab.ID);
        }

        public void Load(IEnumerable<ItemCategory> itemCategories) {
            foreach (var ic in itemCategories)
                ic.CodexTab_Object = _uow.GetRepo<CodexTabRepository>().GetByID(ic.CodexTabID);
        }

        public void Load(ItemCategory itemCategory) {
            itemCategory.CodexTab_Object = _uow.GetRepo<CodexTabRepository>().GetByID(itemCategory.CodexTabID);
        }

        //public void Load(IEnumerable<WarframeItem> items) {
        //    foreach(WarframeItem item in items) {
                
        //    }
        //}

        public void Load(Fish fish, WarframeItemUtilities wiu) {
            if (wiu == null) wiu = new WarframeItemUtilities(_uow);
            fish.WarframeItem_Object = wiu.GetByUniqueName(fish.UniqueName, false);
        }

        public void Load(IEnumerable<Fish> fish) {
            WarframeItemUtilities wiu = new WarframeItemUtilities(_uow);
            foreach(Fish f in fish) {
                Load(f, wiu);
            }
        }
    }
}
