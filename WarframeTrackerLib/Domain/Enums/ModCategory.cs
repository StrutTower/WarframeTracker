using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WarframeTrackerLib.Domain {
    public enum ModCategory {
        Warframe = 1,
        Rifle,
        Shotgun,
        Secondary,
        Melee,
        Sentinel,
        Pets,
        Archwing,
        [Display(Name = "Arch-Gun")]
        ArchGun
    }
}
