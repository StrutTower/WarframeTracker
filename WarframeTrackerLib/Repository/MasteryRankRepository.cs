using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class MasteryRankRepository : DbRepository<MasteryRank> {
        public MasteryRankRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public MasteryRank GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }
    }
}
