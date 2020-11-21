using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using TowerSoft.Repository.Interfaces;
using TowerSoft.Repository.MySql;
using WarframeTrackerLib.Config;

namespace WarframeTrackerLib.Repository {
    public class UnitOfWork : IRepositoryUnitOfWork {
        public UnitOfWork(IOptions<ApplicationSecrets> appSecrets) {
            DbAdapter = new MySqlDbAdapter(appSecrets.Value.ConnectionString);
        }

        public IDbAdapter DbAdapter { get; private set; }

        public void BeginTransaction() {
            DbAdapter.BeginTransaction();
        }

        public void CommitTransaction() {
            DbAdapter.CommitTransaction();
        }

        public void RollbackTransaction() {
            DbAdapter.RollbackTransaction();
        }

        public void Dispose() {
            DbAdapter.Dispose();
        }

        #region Repositories
        private readonly Dictionary<Type, object> _repos = new Dictionary<Type, object>();

        public TRepo GetRepo<TRepo>() {
            Type type = typeof(TRepo);

            if (!_repos.ContainsKey(type)) 
                _repos[type] = Activator.CreateInstance(type, this);
            return (TRepo)_repos[type];
        }
        #endregion
    }
}
