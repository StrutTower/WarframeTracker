using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using TowerSoft.Repository.Interfaces;
using TowerSoft.Repository.MySql;
using WarframeTrackerLib.Config;

namespace WarframeTrackerLib.Repository {
    public class UnitOfWork : IRepositoryUnitOfWork {
        public UnitOfWork(string connectionString) {
            DbAdapter = new MySqlDbAdapter(connectionString);
        }

        public static UnitOfWork CreateNew() {
            string connectionString = ApplicationSecrets.Get().ConnectionString;
            return new UnitOfWork(connectionString);
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
    }
}
