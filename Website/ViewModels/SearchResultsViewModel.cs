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


        public SearchResultsViewModel Load(AdvancedSearchModel model) {
            using (UnitOfWork uow = UnitOfWork.CreateNew()) {
                WarframeItems = new WarframeItemUtilities(uow).AdvancedSearch(model);
                CodexTabs = new CodexTabRepository(uow).GetAll();
                new EagerLoader(uow).Load(CodexTabs);
            }
            return this;
        }
    }
}
