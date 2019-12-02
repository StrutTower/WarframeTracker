using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.Areas.Admin.ViewModels {
    public class ManualItemDataViewModel {
        public List<ManualItemData> ManualItemData { get; set; }

        public Dictionary<string, WarframeItem> ItemDictionary { get; set; }

        public Dictionary<int, ItemCategory> ItemCategoryDictionary { get; set; }

        public Dictionary<int, CodexTab> CodexTabDictionary { get; set; }


        public ManualItemDataViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils) {
                ManualItemData = uow.GetRepo<ManualItemDataRepository>().GetAll();

                ItemDictionary = itemUtils.GetAll().ToDictionary(x => x.UniqueName);
                ItemCategoryDictionary = uow.GetRepo<ItemCategoryRepository>().GetAll().ToDictionary(x => x.ID);
                CodexTabDictionary = uow.GetRepo<CodexTabRepository>().GetAll().ToDictionary(x => x.ID);
            
            return this;
        }
    }
}