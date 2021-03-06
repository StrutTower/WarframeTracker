﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.Areas.Admin.ViewModels {
    public class EditPrimeReleaseViewModel {
        public PrimeRelease PrimeRelease { get; set; }

        public SelectList PrimeSelectList { get; set; }

        public EditPrimeReleaseViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, PrimeRelease pr) {
            PrimeRelease = pr;
            if (pr != null)
                pr.LoadItemNames(uow);
            List<WarframeItem> items = itemUtils.GetByCodexSection(CodexSection.Equipment)
                .Where(x => x.IsPrime)
                .OrderBy(x => x.Name).ToList();
            PrimeSelectList = new SelectList(items, "UniqueName", "Name");
            return this;
        }
    }
}
