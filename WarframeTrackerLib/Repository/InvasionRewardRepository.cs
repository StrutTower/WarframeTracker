using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class InvasionRewardRepository : DbRepository<InvasionReward> {
        public InvasionRewardRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public InvasionReward GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public InvasionReward GetByUniqueName(string uniqueName) {
            return GetSingleEntity(WhereEqual(x => x.UniqueName, uniqueName));
        }
    }
}
