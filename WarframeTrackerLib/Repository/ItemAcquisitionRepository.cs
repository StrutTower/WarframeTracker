using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ItemAcquisitionRepository : DbRepository<ItemAcquisition> {
        public ItemAcquisitionRepository(UnitOfWork unitOfWork) : base(unitOfWork.DbAdapter) { }

        public ItemAcquisition GetByPrimaryKeys(int userID, string itemUniqueName) {
            return GetSingleEntity(new[] {
                WhereEqual(x => x.UserID, userID),
                WhereEqual(x => x.ItemUniqueName, itemUniqueName)
            });
        }

        public List<ItemAcquisition> GetByUserID(int userID) {
            return GetEntities(new WhereCondition("UserID", userID));
        }
    }
}
