using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class CodexViewModel {
        public CodexSection CodexSection { get; set; }

        public CodexTab CurrentCodexTab { get; set; }

        public List<CodexTab> CodexTabs { get; set; }

        public List<WarframeItem> WarframeItems { get; set; }

        public List<ItemAcquisition> ItemAcquisitions { get; set; }

        public Dictionary<string, Fish> Fish { get; set; }


        public CodexViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, ClaimsPrincipal user, CodexSection codexSection) {
            CodexSection = codexSection;

            int? userID = null;
            if (user.Identity.IsAuthenticated) {
                userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            CodexTabs = uow.GetRepo<CodexTabRepository>().GetByCodexSection(codexSection)
                .OrderBy(x => x.SortingPriority).ThenBy(x => x.Name).ToList();

            if (CodexTabs.Any())
                CurrentCodexTab = CodexTabs.FirstOrDefault();

            new EagerLoader(uow).Load(CodexTabs);

            WarframeItems = itemUtils
                .GetByCategoryIDs(CurrentCodexTab.ItemCategory_Objects.Select(x => x.ID))
                .OrderBy(x => x.Name).ToList();

            if (userID.HasValue) {
                ItemAcquisitions = uow.GetRepo<ItemAcquisitionRepository>().GetByUserID(userID.Value);
            }
            if (CodexSection == CodexSection.Fish) {
                Fish = uow.GetRepo<FishRepository>().GetAll().ToDictionary(x => x.UniqueName);
            }
            return this;
        }

        public CodexViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, ClaimsPrincipal user, CodexSection codexSection, int tabID) {
            CodexSection = codexSection;

            int? userID = null;
            if (user.Identity.IsAuthenticated) {
                userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            CodexTabs = uow.GetRepo<CodexTabRepository>().GetByCodexSection(codexSection)
                .OrderBy(x => x.SortingPriority).ThenBy(x => x.Name).ToList();

            CurrentCodexTab = CodexTabs.SingleOrDefault(x => x.ID == tabID);

            new EagerLoader(uow).Load(CodexTabs);

            WarframeItems = itemUtils
                .GetByCategoryIDs(CurrentCodexTab.ItemCategory_Objects.Select(x => x.ID))
                .OrderBy(x => x.Name).ToList();

            if (userID.HasValue) {
                ItemAcquisitions = uow.GetRepo<ItemAcquisitionRepository>().GetByUserID(userID.Value);
            }
            if (CodexSection == CodexSection.Fish) {
                Fish = uow.GetRepo<FishRepository>().GetAll().ToDictionary(x => x.UniqueName);
            }
            return this;
        }
    }
}
