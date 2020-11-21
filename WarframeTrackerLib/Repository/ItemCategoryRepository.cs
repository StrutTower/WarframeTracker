using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ItemCategoryRepository : DbRepository<ItemCategory> {
        public ItemCategoryRepository(UnitOfWork unitOfWork) : base(unitOfWork.DbAdapter) { }

        public ItemCategory GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public List<ItemCategory> GetByCodexTabID(int codexTabID) {
            return GetEntities(WhereEqual(x => x.CodexTabID, codexTabID));
        }

        public List<ItemCategory> GetAssignedToCodexSection(CodexSection codexSection) {
            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += " " +
                "INNER JOIN codextabitemcategory ctic ON " + TableName + ".ID = ctic.ItemCategoryID " +
                "INNER JOIN codextab ct ON ctic.CodexTabID = ct.ID " +
                "WHERE ct.CodexSectionID = @CodexSectionID ";
            query.AddParameter("@CodexSectionID", codexSection);
            return GetEntities(query);
        }

        public List<ItemCategory> GetByCodexSection(CodexSection relics) {
            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += " " +
                "INNER JOIN codextab ct ON "+ TableName + ".CodexTabID=ct.ID " +
                "WHERE ct.CodexSectionID = @CodexSection ";
            query.AddParameter("@CodexSection", (int)relics);
            return GetEntities(query);
        }

        public List<ItemCategory> GetUnassigned() {
            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += " " +
                "LEFT JOIN codextabitemcategory ctic ON " + TableName + ".ID = ctic.ItemCategoryID " +
                "WHERE ctic.CodexTabID IS NULL ";
            return GetEntities(query);
        }
    }
}
