using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ItemAcquisitionRepository : Repository<ItemAcquisition> {
        public ItemAcquisitionRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public ItemAcquisition GetByPrimaryKeys(int userID, string itemUniqueName) {
            return GetSingleEntity(new[] {
                new WhereCondition("UserID", userID),
                new WhereCondition("ItemUniqueName", itemUniqueName)
            });
        }

        public List<ItemAcquisition> GetByUserID(int userID) {
            return GetEntities(new WhereCondition("UserID", userID));
        }
    }
}
