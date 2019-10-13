using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class PrimeReleaseRepository : Repository<PrimeRelease> {
        public PrimeReleaseRepository(IUnitOfWork uow) : base(uow) { }

        public PrimeRelease GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }
    }
}
