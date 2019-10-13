using System;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class UserRepository : Repository<User> {
        public UserRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public User GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public User GetByUsername(string userName) {
            return GetSingleEntity(new WhereCondition("Username", userName));
        }

        public User GetByEmailAddress(string emailAddress) {
            return GetSingleEntity(WhereEqual(x => x.EmailAddress, emailAddress));
        }
    }
}
