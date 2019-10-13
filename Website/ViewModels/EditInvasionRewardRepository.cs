using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.ViewModels {
    public class EditInvasionRewardViewModel {
        public InvasionReward InvasionReward { get; set; }

        public SelectList UniqueNameSelectList { get; set; }


        public EditInvasionRewardViewModel Load(InvasionReward reward = null) {
            InvasionReward = reward;
            List<string> names = new List<string>();

            List<string> datas;
            List<string> existing;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                datas = new TrackedDataRepository(uow).GetGroupedByType("InvasionRewards").Select(x => x.Data).ToList();
                existing = new InvasionRewardRepository(uow).GetAll().Select(x => x.UniqueName).ToList();
            }

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