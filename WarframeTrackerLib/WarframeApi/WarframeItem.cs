using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.WarframeApi {
    public class WarframeItem : IEquatable<WarframeItem> {
        public string UniqueName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ImageName { get; set; }
        public string Category { get; set; }
        public bool? Tradable { get; set; }
        public string WikiaUrl { get; set; }

        public int ItemCategoryID { get; set; }

        //Mods
        public string Polarity { get; set; }
        public string Rarity { get; set; }
        public int? BaseDrain { get; set; }
        public int? FusionLimit { get; set; }

        // Warframe
        public string PassiveDescription { get; set; }
        public double? Sprint { get; set; }
        public string Sex { get; set; }
        public string ReleaseDate { get; set; }
        public string VaultDate { get; set; }
        public string EstimatedVaultDate { get; set; }

        // Weapons
        public double? SecondsPerShot { get; set; }
        public int? MagazineSize { get; set; }
        public double? ReloadTime { get; set; }
        public double? TotalDamage { get; set; }
        public double? DamagePerSeconds { get; set; }
        public string Trigger { get; set; }
        public double? Accuracy { get; set; }
        public double? CriticalChance { get; set; }
        public double? CriticalMultiplier { get; set; }
        public double? ProcChance { get; set; }
        public double? FireRate { get; set; }
        public double? ChargeAttack { get; set; }
        public double? SpinAttack { get; set; }
        public double? LeapAttack { get; set; }
        public double? WallAttack { get; set; }
        public int? Ammo { get; set; }
        public string Damage { get; set; }
        public string Flight { get; set; }
        public string Projectile { get; set; }
        public int? Slot { get; set; }
        public string Noise { get; set; }
        public bool? Sentinel { get; set; }
        public int? MasteryReq { get; set; }
        public double? OmegaAttenuation { get; set; }
        public double? Channeling { get; set; }
        public string Vaulted { get; set; }
        public int? Disposition { get; set; }

        // Vitals
        public int? Health { get; set; }
        public int? Shield { get; set; }
        public int? Armor { get; set; }
        public int? Stamina { get; set; }
        public int? Power { get; set; }

        // Research
        public int? Credits { get; set; }

        //public List<Resource> Resources { get; set; }

        public bool IsExclusive { get; set; }

        public bool IsPrime {
            get {
                if (Name.EndsWith("Prime"))
                    return true;
                return false;
            }
        }

        public bool IsVaulted {
            get {
                if (Vaulted != null && Vaulted.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                    return true;

                if (VaultDate != null && Regex.IsMatch(VaultDate, @"\d\d\d\d \d\d \d\d")) {
                    if (DateTime.TryParse(VaultDate, out DateTime test)) {
                        if (test < DateTime.Now)
                            return true;
                    }
                }

                return false;
            }
        }

        public List<Component> Components { get; set; }
        public List<Drop> Drops { get; set; }
        public DamageTypes DamageTypes { get; set; }


        public bool Equals(WarframeItem other) {
            return other != null && UniqueName == other.UniqueName;
        }

        public override int GetHashCode() {
            return UniqueName.GetHashCode();
        }

        public override string ToString() {
            return Name;
        }
    }
}
