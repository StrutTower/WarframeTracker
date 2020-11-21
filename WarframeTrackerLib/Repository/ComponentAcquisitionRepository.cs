using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ComponentAcquisitionRepository : DbRepository<ComponentAcquisition> {
        public ComponentAcquisitionRepository(UnitOfWork unitOfWork) : base(unitOfWork.DbAdapter) { }

        public ComponentAcquisition GetByPrimaryKeys(int userID, string componentUniqueName, string itemUniqueName) {
            return GetSingleEntity(new[] {
                WhereEqual(x => x.UserID, userID),
                WhereEqual(x => x.ComponentUniqueName, componentUniqueName),
                WhereEqual(x => x.ItemUniqueName, itemUniqueName)
            });
        }

        public List<ComponentAcquisition> GetByUserID(int userID) {
            return GetEntities(WhereEqual(x => x.UserID, userID));
        }

        public List<ComponentAcquisition> GetByItemUniqueName(string uniqueName) {
            return GetEntities(WhereEqual(x => x.ItemUniqueName, uniqueName));
        }

        public List<ComponentAcquisition> GetByItemUniqueNameAndUserID(string uniqueName, int userID) {
            return GetEntities(new[] {
                WhereEqual(x => x.ItemUniqueName, uniqueName),
                WhereEqual(x => x.UserID, userID)
            });
        }
    }
}
