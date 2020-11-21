using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class FishRepository : DbRepository<Fish> {
        public FishRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public Fish GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }
    }
}
