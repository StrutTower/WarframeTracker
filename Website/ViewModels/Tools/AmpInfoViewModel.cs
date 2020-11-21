using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class AmpInfoViewModel {
        public List<AmpComponent> Prisms { get; set; }

        public List<AmpComponent> Scaffolds { get; set; }

        public List<AmpComponent> Braces { get; set; }


        public AmpInfoViewModel Load(UnitOfWork uow) {
            List<AmpComponent> comps = uow.GetRepo<AmpComponentRepository>().GetAll();
            Prisms = comps.Where(x => x.AmpComponentType == AmpComponentType.Prism).OrderBy(x => x.Tier).ToList();
            Scaffolds = comps.Where(x => x.AmpComponentType == AmpComponentType.Scaffold).OrderBy(x => x.Tier).ToList();
            Braces = comps.Where(x => x.AmpComponentType == AmpComponentType.Brace).OrderBy(x => x.Tier).ToList();
            return this;
        }
    }
}
