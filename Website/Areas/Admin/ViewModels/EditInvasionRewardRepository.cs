using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.Areas.Admin.ViewModels {
    public class EditInvasionRewardViewModel {
        public InvasionReward InvasionReward { get; set; }

        public SelectList UniqueNameSelectList { get; set; }


        public EditInvasionRewardViewModel Load(UnitOfWork uow, InvasionReward reward = null) {
            InvasionReward = reward;
            List<string> names = new List<string>();

            List<string> datas = uow.GetRepo<TrackedDataRepository>().GetGroupedByType("InvasionRewards").Select(x => x.Data).ToList();
            List<string> existing = uow.GetRepo<InvasionRewardRepository>().GetAll().Select(x => x.UniqueName).ToList();

            foreach(string data in datas) {
                if (!existing.Any(x => x.Equals(data))) {
                    names.Add(data);
                }
            }

            UniqueNameSelectList = new SelectList(names.OrderBy(x => x));

            return this;
        }
    }
}