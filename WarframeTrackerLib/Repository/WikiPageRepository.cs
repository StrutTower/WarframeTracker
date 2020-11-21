using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Repository {
    public class WikiPageRepository : DbRepository<WikiPage> {
        public WikiPageRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public WikiPage GetByID(int id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }

        public List<WikiPage> GetByWikiPageType(WikiPageType type) {
            return GetEntities(WhereEqual(x => x.WikiPageType, type));
        }

        public List<WikiPage> GetIndexPages() {
            return GetEntities(WhereEqual(x => x.IsIndexPage, true));
        }

        public WikiPage GetIndexPageByType(WikiPageType type) {
            return GetSingleEntity(new[] {
                WhereEqual(x => x.WikiPageType, type),
                WhereEqual(x => x.IsIndexPage, true)
            });
        }
    }
}
