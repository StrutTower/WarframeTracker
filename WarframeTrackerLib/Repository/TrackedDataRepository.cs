using System.Collections.Generic;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class TrackedDataRepository : DbRepository<TrackedData> {
        public TrackedDataRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public TrackedData GetByID(int id) {
            return GetSingleEntity(WhereEqual(x=> x.ID, id));
        }

        public List<TrackedData> GetByType(string type) {
            return GetEntities(WhereEqual(x => x.Type, type));
        }

        public List<TrackedData> GetByDataID(string dataID) {
            return GetEntities(WhereEqual(x => x.DataID, dataID));
        }

        public List<TrackedData> GetGroupedByType(string type) {
            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += "" +
                "WHERE Type = @Type " +
                "GROUP BY Data ";
            query.AddParameter("@Type", type);
            return GetEntities(query);
        }
    }
}
