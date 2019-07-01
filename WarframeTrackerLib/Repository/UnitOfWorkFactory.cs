using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using TowerSoft.Repository.MySql;
using WarframeTrackerLib.Config;

namespace WarframeTrackerLib.Repository {
    public class UnitOfWorkFactory {
        public string ConnectionString { get; }

        public IUnitOfWork UnitOfWork { get; set; }

        public UnitOfWorkFactory() {
            ConnectionString = ApplicationSecrets.Get().ConnectionString;
            UnitOfWork = new MySqlUnitOfWork(ConnectionString);
        }
    }
}
