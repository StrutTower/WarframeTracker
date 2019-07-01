using System.Collections.Generic;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class SentNotificationRepository : Repository<SentNotification> {
        public SentNotificationRepository(IUnitOfWork uow) : base(uow) { }

        public SentNotification GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public List<SentNotification> GetByType(string type) {
            return GetEntities(new WhereCondition("Type", type));
        }
    }
}
