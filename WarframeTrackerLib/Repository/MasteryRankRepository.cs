using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class MasteryRankRepository : Repository<MasteryRank> {
        public MasteryRankRepository(IUnitOfWork uow) : base(uow) { }

        public MasteryRank GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }
    }
}
