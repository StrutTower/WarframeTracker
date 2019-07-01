using System;
using System.Collections.Generic;
using System.Linq;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class CodexTabRepository : Repository<CodexTab> {
        public CodexTabRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public CodexTab GetByID(int id) {
            return GetSingleEntity(new WhereCondition("ID", id));
        }

        public List<CodexTab> GetByCodexSection(CodexSection codexSection) {
            return GetEntities(new WhereCondition("CodexSectionID", codexSection));
        }
    }
}
