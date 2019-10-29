using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class ManualItemDataViewModel {
        public List<ManualItemData> ManualItemData { get; set; }

        public Dictionary<string, WarframeItem> ItemDictionary { get; set; }

        public Dictionary<int, ItemCategory> ItemCategoryDictionary { get; set; }

        public Dictionary<int, CodexTab> CodexTabDictionary { get; set; }


        public ManualItemDataViewModel Load() {
            using (UnitOfWork uow = UnitOfWork.CreateNew()) {
                ManualItemData = new ManualItemDataRepository(uow).GetAll();

                ItemDictionary = new WarframeItemUtilities(uow).GetAll().ToDictionary(x => x.UniqueName);
                ItemCategoryDictionary = new ItemCategoryRepository(uow).GetAll().ToDictionary(x => x.ID);
                CodexTabDictionary = new CodexTabRepository(uow).GetAll().ToDictionary(x => x.ID);
            }

            return this;
        }
    }
}