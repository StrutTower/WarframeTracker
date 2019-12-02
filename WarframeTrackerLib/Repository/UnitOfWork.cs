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

        //public static UnitOfWork CreateNew() {
        //    string connectionString = ApplicationSecrets.Get().ConnectionString;
        //    //return new UnitOfWork(new ApplicationSecrets());
        //    return null;
        //}

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

            if (!_repos.ContainsKey(type)) _repos[type] = Activator.CreateInstance(type, this);
            return (TRepo)_repos[type];
        }
        #endregion

        #region Repositories
        //private CodexTabRepository _codexTabRepo;
        //public CodexTabRepository CodexTabRepository {
        //    get {
        //        if (_codexTabRepo == null) _codexTabRepo = new CodexTabRepository(this);
        //        return _codexTabRepo;
        //    }
        //}

        //private ComponentAcquisitionRepository _componentAcquisitionRepo;
        //public ComponentAcquisitionRepository ComponentAcquisitionRepository {
        //    get {
        //        if (_componentAcquisitionRepo == null) _componentAcquisitionRepo = new ComponentAcquisitionRepository(this);
        //        return _componentAcquisitionRepo;
        //    }
        //}

        //private FishRepository _fishRepo;
        //public FishRepository FishRepository {
        //    get {
        //        if (_fishRepo == null) _fishRepo = new FishRepository(this);
        //        return _fishRepo;
        //    }
        //}

        //private InvasionRewardRepository _invasionRewardRepo;
        //public InvasionRewardRepository InvasionRewardRepository {
        //    get {
        //        if (_invasionRewardRepo == null) _invasionRewardRepo = new InvasionRewardRepository(this);
        //        return _invasionRewardRepo;
        //    }
        //}

        //private ItemAcquisitionRepository _itemAcquisitionRepo;
        //public ItemAcquisitionRepository ItemAcquisitionRepository {
        //    get {
        //        if (_itemAcquisitionRepo == null) _itemAcquisitionRepo = new ItemAcquisitionRepository(this);
        //        return _itemAcquisitionRepo;
        //    }
        //}

        //private ItemCacheRepository _itemCacheRepo;
        //public ItemCacheRepository ItemCacheRepository {
        //    get {
        //        if (_itemCacheRepo == null) _itemCacheRepo = new ItemCacheRepository(this);
        //        return _itemCacheRepo;
        //    }
        //}

        //private ItemCategoryRepository _itemCategoryRepo;
        //public ItemCategoryRepository ItemCategoryRepository {
        //    get {
        //        if (_itemCategoryRepo == null) _itemCategoryRepo = new ItemCategoryRepository(this);
        //        return _itemCategoryRepo;
        //    }
        //}

        //private ManualItemDataRepository _manualItemDataRepo;
        //public ManualItemDataRepository ManualItemDataRepository {
        //    get {
        //        if (_manualItemDataRepo == null) _manualItemDataRepo = new ManualItemDataRepository(this);
        //        return _manualItemDataRepo;
        //    }
        //}

        //private PrimeReleaseRepository _primeReleaseRepo;
        //public PrimeReleaseRepository PrimeReleaseRepository {
        //    get {
        //        if (_primeReleaseRepo == null) _primeReleaseRepo = new PrimeReleaseRepository(this);
        //        return _primeReleaseRepo;
        //    }
        //}

        //private SentNotificationRepository _sentNotificationRepo;
        //public SentNotificationRepository SentNotificationRepository {
        //    get {
        //        if (_sentNotificationRepo == null) _sentNotificationRepo = new SentNotificationRepository(this);
        //        return _sentNotificationRepo;
        //    }
        //}

        //private TrackedDataRepository _trackedDataRepo;
        //public TrackedDataRepository TrackedDataRepository {
        //    get {
        //        if (_trackedDataRepo == null) _trackedDataRepo = new TrackedDataRepository(this);
        //        return _trackedDataRepo;
        //    }
        //}

        //private UserDataRepository _userDataRepo;
        //public UserDataRepository UserDataRepository {
        //    get {
        //        if (_userDataRepo == null) _userDataRepo = new UserDataRepository(this);
        //        return _userDataRepo;
        //    }
        //}

        //private UserRepository _userRepo;
        //public UserRepository UserRepository {
        //    get {
        //        if (_userRepo == null) _userRepo = new UserRepository(this);
        //        return _userRepo;
        //    }
        //}
        #endregion
    }
}
