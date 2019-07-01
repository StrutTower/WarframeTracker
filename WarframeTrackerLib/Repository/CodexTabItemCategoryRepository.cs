using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    //public class CodexTabItemCategoryRepository : Repository<CodexTabItemCategory> {
    //    public CodexTabItemCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    //    public CodexTabItemCategory GetByPrimaryKeys(int codexTabID, int itemCategoryID) {
    //        return GetSingleEntity(new[] {
    //            new WhereCondition("CodexTabID", codexTabID),
    //            new WhereCondition("ItemCategoryID", itemCategoryID)
    //        });
    //    }

    //    public List<CodexTabItemCategory> GetByCodexTabID(int codexTabID) {
    //        return GetEntities(new WhereCondition("CodexTabID", codexTabID));
    //    }

    //    public List<CodexTabItemCategory> GetByItemCategoryID(int itemCategoryID) {
    //        return GetEntities(new WhereCondition("ItemCategoryID", itemCategoryID));
    //    }
    //}
}
