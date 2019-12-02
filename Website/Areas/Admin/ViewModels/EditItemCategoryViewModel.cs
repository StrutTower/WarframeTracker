using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.Areas.Admin.ViewModels {
    public class EditItemCategoryViewModel {

        public ItemCategory ItemCategory { get; set; }

        public SelectList CodexTabSelectList { get; set; }


        public EditItemCategoryViewModel(UnitOfWork uow, ItemCategory itemCategory) {
            ItemCategory = itemCategory;

            List<CodexTab> codexTabs = uow.GetRepo<CodexTabRepository>().GetAll();

            CodexTabSelectList = new SelectList(codexTabs.OrderBy(x => x.CodexSectionID.ToString()).ThenBy(x => x.Name), "ID", "Name", null, "CodexSectionID");
        }
    }
}
