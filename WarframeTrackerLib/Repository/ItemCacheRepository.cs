using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.Repository {
    public class ItemCacheRepository : Repository<ItemCache> {
        public ItemCacheRepository(UnitOfWork unitOfWork) : base(unitOfWork.DbAdapter) { }

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
            foreach (int categoryID in categoryIDs) {
                inList.Add("@" + counter);
                query.AddParameter("@" + counter, categoryID);
                counter++;
            }
            query.SqlQuery += string.Join(",", inList) + ") ";
            return GetEntities(query);
        }

        public List<ItemCache> GetByCodexSection(CodexSection codexSection) {
            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += "" +
                "INNER JOIN itemcategory ic ON " + TableName + ".ItemCategoryID = ic.ID " +
                "INNER JOIN codextab ct ON ic.CodexTabID = ct.ID " +
                "WHERE ct.CodexSectionID = @CodexSection ";
            query.AddParameter("@CodexSection", codexSection);
            return GetEntities(query);
        }

        public List<ItemCache> Search(string criteria) {
            return GetEntities(new WhereCondition("Name", criteria, Comparison.LikeBothSidesWildcard));
        }

        public void RemoveAll() {
            string query = "DELETE FROM " + TableName;
            GetDbConnection().Execute(query);
        }

        public List<ItemCache> AdvancedSearch(AdvancedSearchModel model) {
            QueryBuilder query = GetQueryBuilder();
            query.SqlQuery += "" +
                $"INNER JOIN itemcategory ic ON {TableName}.ItemCategoryID = ic.ID " +
                $"INNER JOIN codextab ct ON ct.ID = ic.CodexTabID ";

            List<string> whereConditions = new List<string>();
            if (!string.IsNullOrWhiteSpace(model.Name)) {
                whereConditions.Add(TableName + ".Name LIKE @Name");
                query.AddParameter("@Name", $"%{model.Name}%");
            }

            if (model.CodexSection != null) {
                whereConditions.Add("ct.CodexSectionID = @CodexSection");
                query.AddParameter("@CodexSection", model.CodexSection);
            }

            if (!string.IsNullOrWhiteSpace(model.MasteryRequirement)) {
                List<string> parts = new List<string>();
                int counter = 0;

                List<string> masteryParts = model.GetMasteryRequirementParts();
                if (masteryParts != null) {
                    foreach (string queryPart in masteryParts) {
                        if (queryPart.Contains("-")) {
                            string[] startEndRange = queryPart.Split('-');
                            int start = int.Parse(startEndRange[0]);
                            int end = int.Parse(startEndRange[1]);
                            parts.Add($"(MasteryRequired >= @Mastery{counter}Start AND MasteryRequired <= @Mastery{counter}End)");
                            query.AddParameter($"@Mastery{counter}Start", start);
                            query.AddParameter($"@Mastery{counter}End", end);

                        } else if (queryPart.Contains(">")) {
                            int number = int.Parse(queryPart.Replace(">", ""));
                            parts.Add($"MasteryRequired > @Mastery{counter}");
                            query.AddParameter($"Mastery{counter}", number);

                        } else if (queryPart.Contains("<")) {
                            int number = int.Parse(queryPart.Replace("<", ""));
                            parts.Add($"MasteryRequired < @Mastery{counter}");
                            query.AddParameter($"@Mastery{counter}", number);

                        } else {
                            int number = int.Parse(queryPart);
                            parts.Add($"MasteryRequired = @Mastery{counter}");
                            query.AddParameter($"@Mastery{counter}", number);
                        }

                        counter++;
                    }
                    whereConditions.Add("(" + string.Join(" OR ", parts) + ")");
                }
            }

            query.SqlQuery += "WHERE " + string.Join(" AND ", whereConditions);

            return GetEntities(query);
        }
    }
}
