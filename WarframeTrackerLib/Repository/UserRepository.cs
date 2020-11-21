using System;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class UserRepository : DbRepository<User> {
        public UserRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public User GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public User GetByUsername(string userName) {
            return GetSingleEntity(WhereEqual(x => x.Username, userName));
        }

        public User GetByEmailAddress(string emailAddress) {
            return GetSingleEntity(WhereEqual(x => x.EmailAddress, emailAddress));
        }
    }
}
