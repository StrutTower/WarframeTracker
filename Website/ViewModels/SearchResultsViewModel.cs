using System;
using System.Collections.Generic;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class SearchResultsViewModel {
        public List<WarframeItem> WarframeItems { get; set; }

        public List<CodexTab> CodexTabs { get; set; }


        public SearchResultsViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, AdvancedSearchModel model) {
                WarframeItems = itemUtils.AdvancedSearch(model);
                CodexTabs = uow.GetRepo<CodexTabRepository>().GetAll();
                new EagerLoader(uow).Load(CodexTabs);
            return this;
        }
    }
}
