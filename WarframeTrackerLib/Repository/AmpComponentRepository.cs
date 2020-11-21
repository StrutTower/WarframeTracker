using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class AmpComponentRepository : DbRepository<AmpComponent> {
        public AmpComponentRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public AmpComponent GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }
    }
}
