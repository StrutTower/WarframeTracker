using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ManualItemDataRepository : Repository<ManualItemData> {
        public ManualItemDataRepository(IUnitOfWork uow) : base(uow) { }

        public ManualItemData GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public List<ManualItemData> GetByItemUniqueName(string itemUniqueName) {
            return GetEntities(new WhereCondition("ItemUniqueName", itemUniqueName));
        }
    }
}
