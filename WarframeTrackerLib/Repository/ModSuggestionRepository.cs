using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
   public class ModSuggestionRepository : DbRepository<ModSuggestion> {
        public ModSuggestionRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public ModSuggestion GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public List<ModSuggestion> GetByPredecessorID(int predecessorID) {
            return GetEntities(WhereEqual(x => x.PredecessorModSuggestionID, predecessorID));
        }
    }
}
