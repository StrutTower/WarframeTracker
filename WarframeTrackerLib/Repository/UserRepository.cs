using System;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class UserRepository : Repository<User> {
        public UserRepository(IUnitOfWork uow) : base(uow) { }

        public User GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public User GetByUsername(string userName) {
            return GetSingleEntity(new WhereCondition("Username", userName));
        }
    }
}
