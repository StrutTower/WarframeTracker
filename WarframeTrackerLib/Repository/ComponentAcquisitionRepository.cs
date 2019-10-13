using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ComponentAcquisitionRepository : Repository<ComponentAcquisition> {
        public ComponentAcquisitionRepository(UnitOfWork unitOfWork) : base(unitOfWork.DbAdapter) { }

        public ComponentAcquisition GetByPrimaryKeys(int userID, string componentUniqueName, string itemUniqueName) {
            return GetSingleEntity(new[] {
                new WhereCondition("UserID", userID),
                new WhereCondition("ComponentUniqueName", componentUniqueName),
                new WhereCondition("ItemUniqueName", itemUniqueName)
            });
        }

        public List<ComponentAcquisition> GetByUserID(int userID) {
            return GetEntities(new WhereCondition("UserID", userID));
        }

        public List<ComponentAcquisition> GetByItemUniqueName(string uniqueName) {
            return GetEntities(new WhereCondition("ItemUniqueName", uniqueName));
        }

        public List<ComponentAcquisition> GetByItemUniqueNameAndUserID(string uniqueName, int userID) {
            return GetEntities(new[] {
                WhereEqual(x => x.ItemUniqueName, uniqueName),
                WhereEqual(x => x.UserID, userID)
            });
        }
    }
}
