using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class UserDataRepository : Repository<UserData> {
        public UserDataRepository(IUnitOfWork uow) : base(uow) { }

        public UserData GetByUserID(int userID) {
            return GetSingleEntity(new WhereCondition("UserID", userID));
        }
    }
}
