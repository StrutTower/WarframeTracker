using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class UserDataRepository : DbRepository<UserData> {
        public UserDataRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public UserData GetByUserID(int userID) {
            return GetSingleEntity(WhereEqual(x => x.UserID, userID));
        }
    }
}
