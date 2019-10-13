using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class InvasionRewardRepository : Repository<InvasionReward> {
        public InvasionRewardRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public InvasionReward GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public InvasionReward GetByUniqueName(string uniqueName) {
            return GetSingleEntity(new WhereCondition("UniqueName", uniqueName));
        }
    }
}
