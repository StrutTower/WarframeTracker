using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class CodexModsViewModel {
        public List<WarframeItem> Mods { get; set; }

        public List<ItemAcquisition> ItemAcquisitions { get; set; } = new List<ItemAcquisition>();

        public int TabID { get; set; }

        public CodexModsViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, ClaimsPrincipal user, int tabID = 1) {
            TabID = tabID;
            List<WarframeItem> allMods = itemUtils.GetByCodexSection(CodexSection.Mods);

            switch (tabID) {
                case 1:
                    Mods = allMods.Where(x => x.CompatName == "WARFRAME" || x.CompatName == "AURA").ToList();
                    break;
                case 2:
                    Mods = allMods.Where(x => x.IsAugment == true).ToList();
                    break;
                case 3:
                    List<string> compats = new List<string> { "PRIMARY", "RIFLE", "RIFLE (NO AOE)", "ASSAULT RIFLE", "SHOTGUN", "BOW", "SNIPER" };
                    Mods = allMods.Where(x => compats.Contains(x.CompatName)).ToList();
                    break;
                case 4:
                    Mods = allMods.Where(x => x.CompatName == "PISTOL" | x.CompatName == "PISTOL (NO AOE)").ToList();
                    break;
                case 5:
                    Mods = allMods.Where(x => x.CompatName == "MELEE" || x.CompatName == "THROWN MELEE").ToList();
                    break;
                case 6:
                    Mods = allMods.Where(x => x.CompatName == "ARCHWING").ToList();
                    break;
                case 7:
                    Mods = allMods.Where(x => x.CompatName == "ARCHGUN").ToList();
                    break;
                case 8:
                    Mods = allMods.Where(x => x.CompatName == "ARCHMELEE").ToList();
                    break;
                case 9:
                    List<string> compats2 = new List<string> { "BEAST", "COMPANION", "ROBOTIC", "SENTINEL" };
                    Mods = allMods.Where(x => compats2.Contains(x.CompatName)).ToList();
                    break;
                case 10:
                    Mods = allMods.Where(x => x.CompatName == "K-Drive").ToList();
                    break;
                case 11:
                    Mods = allMods.Where(x => x.CompatName == "NECRAMECH").ToList();
                    break;
                case 12:
                    Mods = allMods.Where(x => x.CompatName == "PARAZON").ToList();
                    break;
            }

            int? userID = null;
            if (user.Identity.IsAuthenticated) {
                userID = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            if (userID.HasValue) {
                ItemAcquisitions = uow.GetRepo<ItemAcquisitionRepository>().GetByUserID(userID.Value);
            }

            return this;
        }
    }
}
