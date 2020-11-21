using System;
using System.Collections.Generic;
using System.Linq;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class CodexTabRepository : DbRepository<CodexTab> {
        public CodexTabRepository(UnitOfWork unitOfWork) : base(unitOfWork.DbAdapter) { }

        public CodexTab GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public List<CodexTab> GetByCodexSection(CodexSection codexSection) {
            return GetEntities(WhereEqual(x => x.CodexSectionID, codexSection));
        }
    }
}
