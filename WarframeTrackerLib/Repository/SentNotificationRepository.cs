using System;
using System.Collections.Generic;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class SentNotificationRepository : DbRepository<SentNotification> {
        public SentNotificationRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public SentNotification GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id)); ;
        }

        public List<SentNotification> GetByType(string type) {
            return GetEntities(WhereEqual(x => x.Type, type));
        }

        public SentNotification GetByDataID(string oid) {
            return GetSingleEntity(WhereEqual(x => x.DataID, oid)); ;
        }
    }
}
