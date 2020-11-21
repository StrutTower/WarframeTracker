using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ManualItemDataRepository : DbRepository<ManualItemData> {
        public ManualItemDataRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public ManualItemData GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public List<ManualItemData> GetByItemUniqueName(string itemUniqueName) {
            return GetEntities(WhereEqual(x => x.ItemUniqueName, itemUniqueName));
        }
    }
}
