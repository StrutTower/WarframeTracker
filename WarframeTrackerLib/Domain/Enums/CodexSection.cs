using System.ComponentModel.DataAnnotations;

namespace WarframeTrackerLib.Domain {
    public enum CodexSection {
        Equipment = 1,
        Relics,
        Mods,
        [Display(Name = "Captura Scenes")]
        CapturaScenes,
        Fish
    }
}
