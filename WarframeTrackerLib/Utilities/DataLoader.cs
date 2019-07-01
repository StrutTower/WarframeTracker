using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace WarframeTrackerLib.Utilities {
    public class EagerLoader {
        IUnitOfWork _uow;

        public EagerLoader(IUnitOfWork uow) {
            _uow = uow;
        }

        public void Load(IEnumerable<CodexTab> codexTabs) {
            ItemCategoryRepository itemCategoryRepo = new ItemCategoryRepository(_uow);

            foreach (var tab in codexTabs)
                tab.ItemCategory_Objects = itemCategoryRepo.GetByCodexTabID(tab.ID);
        }

        public void Load(CodexTab codexTab) {
            ItemCategoryRepository itemCategoryRepo = new ItemCategoryRepository(_uow);
            codexTab.ItemCategory_Objects = itemCategoryRepo.GetByCodexTabID(codexTab.ID);
        }

        public void Load(IEnumerable<ItemCategory> itemCategories) {
            CodexTabRepository repo = new CodexTabRepository(_uow);

            foreach (var ic in itemCategories)
                ic.CodexTab_Object = repo.GetByID(ic.CodexTabID);
        }

        public void Load(ItemCategory itemCategory) {
            CodexTabRepository repo = new CodexTabRepository(_uow);
            itemCategory.CodexTab_Object = repo.GetByID(itemCategory.CodexTabID);
        }
    }
}
