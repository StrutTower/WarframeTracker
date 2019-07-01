using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class InvasionRewardRepository : Repository<InvasionReward> {
        public InvasionRewardRepository(IUnitOfWork uow) : base(uow) { }

        public InvasionReward GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public InvasionReward GetByUniqueName(string uniqueName) {
            return GetSingleEntity(new WhereCondition("UniqueName", uniqueName));
        }
    }
}
