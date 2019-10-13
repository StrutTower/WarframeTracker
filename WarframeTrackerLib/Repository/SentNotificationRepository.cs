using System;
using System.Collections.Generic;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class SentNotificationRepository : Repository<SentNotification> {
        public SentNotificationRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public SentNotification GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public List<SentNotification> GetByType(string type) {
            return GetEntities(new WhereCondition("Type", type));
        }

        public object GetByDataID(string oid) {
            return GetSingleEntity(WhereEqual(x => x.DataID, oid)); ;
        }
    }
}
