using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class PrimeReleaseRepository : DbRepository<PrimeRelease> {
        public PrimeReleaseRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public PrimeRelease GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }
    }
}
