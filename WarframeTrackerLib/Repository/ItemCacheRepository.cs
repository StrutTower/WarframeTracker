using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class ItemCacheRepository : Repository<ItemCache> {
        public ItemCacheRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public ItemCache GetByUniqueName(string uniqueName) {
            return GetSingleEntity(new WhereCondition("UniqueName", uniqueName));
        }

        public List<ItemCache> GetByItemCategoryID(int categoryID) {
            return GetEntities(new WhereCondition("ItemCategoryID", categoryID));
        }

        public List<ItemCache> GetByItemCategoryIDs(IEnumerable<int> categoryIDs) {
            if (categoryIDs == null || !categoryIDs.Any()) return null;

            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += " " +
                "WHERE ItemCategoryID IN (";

            int counter = 0;
            List<string> inList = new List<string>();
            foreach(int categoryID in categoryIDs) {
                inList.Add("@" + counter);
                query.AddParameter("@" + counter, categoryID);
                counter++;
            }
            query.SqlQuery += string.Join(",", inList) + ") ";
            return GetEntities(query);
        }

        public List<ItemCache> Search(string criteria) {
            return GetEntities(new WhereCondition("Name", criteria, Comparison.LikeBothSidesWildcard));
        }

        public void RemoveAll() {
            string query = "DELETE FROM " + Mappings.TableName;
            GetDbConnection().Execute(query);
        }
    }
}
