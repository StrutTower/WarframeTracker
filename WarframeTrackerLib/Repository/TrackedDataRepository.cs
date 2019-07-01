using System.Collections.Generic;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class TrackedDataRepository : Repository<TrackedData> {
        public TrackedDataRepository(IUnitOfWork uow) : base(uow) { }

        public TrackedData GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public List<TrackedData> GetByType(string type) {
            return GetEntities(new WhereCondition("Type", type));
        }

        public List<TrackedData> GetByDataID(string dataID) {
            return GetEntities(new WhereCondition("DataID", dataID));
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
