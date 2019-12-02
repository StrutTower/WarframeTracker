using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TowerSoft.Repository;
using TowerSoft.Repository.Attributes;
using WarframeTrackerLib.Repository;

namespace WarframeTrackerLib.Domain {
    public class PrimeRelease {
        [Autonumber]
        public int ID { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime? VaultedDate { get; set; }

        [Required]
        public string WarframeUniqueName { get; set; }

        [Required]
        public string Item1UniqueName { get; set; }

        public string Item2UniqueName { get; set; }

        public bool IsReleasedFromVault { get; set; }


        /// <summary>
        /// Not Mapped. Display name of the Warframe
        /// </summary>
        [NotMapped]
        public string WarframeName { get; set; }

        /// <summary>
        /// Not Mapped. Display name of the first item
        /// </summary>
        [NotMapped]
        public string Item1Name { get; set; }

        /// <summary>
        /// Not Mapped. Display name of the second item
        /// 
        /// </summary>
        [NotMapped]
        public string Item2Name { get; set; }


        public void LoadItemNames(UnitOfWork uow) {
            WarframeName = GetItemName(uow.GetRepo<ItemCacheRepository>(), WarframeUniqueName);
            Item1Name = GetItemName(uow.GetRepo<ItemCacheRepository>(), Item1UniqueName);
            Item2Name = GetItemName(uow.GetRepo<ItemCacheRepository>(), Item2UniqueName);
        }

        private string GetItemName(ItemCacheRepository repo, string uniqueName) {
            if (string.IsNullOrWhiteSpace(uniqueName)) return string.Empty;

            ItemCache cache = repo.GetByUniqueName(uniqueName);
            if (cache == null)
                return string.Empty;
            else
                return cache.Name;
        }
    }
}
