﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WesternStateHospital.WSHUtilities;

namespace WarframeTrackerLib.WarframeApi {
    public class WarframeItemUtilities {
        private readonly string _allItemsUrl =
            "https://raw.githubusercontent.com/WFCD/warframe-items/development/data/json/All.json";

        UnitOfWork _unitOfWork;

        public WarframeItemUtilities(UnitOfWork uow) {
            _unitOfWork = uow;
        }

        #region Get Methods
        public WarframeItem GetByUniqueName(string uniqueName, bool redownloadCacheIfNotFound = true) {
            ItemCache itemCache = new ItemCacheRepository(_unitOfWork).GetByUniqueName(uniqueName);
            if (redownloadCacheIfNotFound && itemCache == null || itemCache.UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                itemCache = RedownloadCache().SingleOrDefault(x => x.UniqueName == uniqueName);
            }
            return JsonConvert.DeserializeObject<WarframeItem>(itemCache.Data);
        }

        public List<WarframeItem> GetByName(string name) {
            List<ItemCache> caches = new ItemCacheRepository(_unitOfWork).GetByName(name);

            if (caches.SafeAny() && caches.First().UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                caches = RedownloadCache().Where(x => x.Name == name).ToList();
            }
            string test = "[" + string.Join(",", caches.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> GetByCodexSection(CodexSection codexSection) {
            List<ItemCache> caches = new ItemCacheRepository(_unitOfWork).GetByCodexSection(codexSection);

            if (caches == null && !caches.Any() || caches.First().UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                List<ItemCategory> itemCategories = new ItemCategoryRepository(_unitOfWork).GetByCodexSection(codexSection);
                caches = RedownloadCache().Where(x => itemCategories.Select(y => y.ID).Contains(x.ItemCategoryID)).ToList();
            }
            string test = "[" + string.Join(",", caches.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> GetByCategoryID(int categoryID) {
            List<ItemCache> ic = new ItemCacheRepository(_unitOfWork).GetByItemCategoryID(categoryID);

            // Redownload Cached Items
            if (ic == null || !ic.Any() || ic.First().UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                ic = RedownloadCache().Where(x => x.ItemCategoryID == categoryID).ToList();
            }
            string test = "[" + string.Join(",", ic.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> GetByCategoryIDs(IEnumerable<int> itemCategoriesIDs) {
            List<ItemCache> ics = new ItemCacheRepository(_unitOfWork).GetByItemCategoryIDs(itemCategoriesIDs);

            // Redownload Cached Items
            if (ics == null || !ics.Any() || ics.First().UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                ics = RedownloadCache().Where(x => itemCategoriesIDs.Contains(x.ItemCategoryID)).ToList();
            }
            string test = "[" + string.Join(",", ics.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> GetAll() {
            List<ItemCache> ic = new ItemCacheRepository(_unitOfWork).GetAll();

            // Redownload Cached Items
            if (ic == null || !ic.Any() || ic.First().UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                ic = RedownloadCache();
            }
            string test = "[" + string.Join(",", ic.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> Search(string q) {
            List<ItemCache> ics = new ItemCacheRepository(_unitOfWork).Search(q);

            // Redownload Cached Items
            if (ics == null || !ics.Any() || ics.First().UpdatedTimestamp < DateTime.Now.AddDays(-2)) {
                ics = RedownloadCache().Where(x => x.Name.Contains(q)).ToList();
            }
            string test = "[" + string.Join(",", ics.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> AdvancedSearch(AdvancedSearchModel model) {
            List<ItemCache> ics = new ItemCacheRepository(_unitOfWork).AdvancedSearch(model);

            string test = "[" + string.Join(",", ics.Select(x => x.Data)) + "]";
            return JsonConvert.DeserializeObject<List<WarframeItem>>(test);
        }

        public List<WarframeItem> GetAllMods() {
            using WebClient client = new WebClient();
            string json = client.DownloadString("https://raw.githubusercontent.com/WFCD/warframe-items/development/data/json/Mods.json");
            List<WarframeItem> items = JsonConvert.DeserializeObject<List<WarframeItem>>(json);
            return items;
        }
        #endregion

        public List<ItemCache> RedownloadCache() {
            _unitOfWork.BeginTransaction();
            try {
                List<ManualItemData> manualItemData = _unitOfWork.GetRepo<ManualItemDataRepository>().GetAll();
                IEnumerable<ItemCategory> itemCategories = _unitOfWork.GetRepo<ItemCategoryRepository>().GetAll().OrderBy(x => x.SortingPriority);
                new EagerLoader(_unitOfWork).Load(itemCategories);

                List<WarframeItem> items = GetWfcdItems(itemCategories);

                //ItemCacheRepository repo = new ItemCacheRepository(uow);
                _unitOfWork.GetRepo<ItemCacheRepository>().RemoveAll();
                List<ItemCache> itemCaches = new List<ItemCache>();
                foreach (WarframeItem item in items) {
                    if (item.Name.Contains("'S")) {
                        item.Name = item.Name.Replace("'S", "'s");
                    }
                    // Manual Item Data
                    List<ManualItemData> manual = manualItemData.Where(x => x.ItemUniqueName == item.UniqueName).ToList();
                    foreach (ManualItemData data in manual) {
                        var prop = item.GetType().GetProperty(data.PropertyName);
                        try {
                            prop.SetValue(item, Convert.ChangeType(data.Value, prop.PropertyType));
                        } catch (Exception ex) {
#if DEBUG
                            throw new Exception($"Failed to set {prop.Name} with value {data.Value}", ex);
#endif
                        }
                    }

                    ItemCache itemCache = new ItemCache {
                        UniqueName = item.UniqueName,
                        ItemCategoryID = item.ItemCategoryID,
                        Name = item.Name,
                        MasteryRequired = item.MasteryReq,
                        UpdatedTimestamp = DateTime.Now
                    };
                    itemCache.Data = JsonConvert.SerializeObject(item);
                    itemCaches.Add(itemCache);
                }

                foreach (ItemCache cache in itemCaches.GroupBy(x => x.UniqueName).Select(x => x.First())) {
                    _unitOfWork.GetRepo<ItemCacheRepository>().Add(cache);
                }
                _unitOfWork.CommitTransaction();

                return itemCaches;
            } catch {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private List<WarframeItem> GetWfcdItems(IEnumerable<ItemCategory> itemCategories) {
            List<WarframeItem> output = new List<WarframeItem>();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            List<WarframeItem> items;
            using (WebClient client = new WebClient { UseDefaultCredentials = true }) {
                string json = client.DownloadString(_allItemsUrl);
                items = JsonConvert.DeserializeObject<List<WarframeItem>>(json);
            }

            foreach (ItemCategory itemCategory in itemCategories) {

                var matchingItems = items.Where(x => x.Category == itemCategory.WarframeApiCategoryName).ToList();
                if (!string.IsNullOrWhiteSpace(itemCategory.WarframeApiProductCategoryName)) {
                    matchingItems = matchingItems.Where(x => x.ProductCategory == itemCategory.WarframeApiProductCategoryName).ToList();
                }
                if (!string.IsNullOrWhiteSpace(itemCategory.WarframeApiUniqueNameRegexFilter)) {
                    matchingItems = matchingItems
                        .Where(x => Regex.IsMatch(x.UniqueName, itemCategory.WarframeApiUniqueNameRegexFilter)).ToList();
                }

                foreach (WarframeItem item in matchingItems) {
                    if (itemCategory.ManuallyExcludedUniqueNamesList.Contains(item.UniqueName))
                        continue;

                    item.ItemCategoryID = itemCategory.ID;
                    if (itemCategory.CodexTab_Object.CodexSectionID == CodexSection.Relics) {
                        // Mark the relic as vaulted if it does not have any drop locations
                        if (item.Drops == null || !item.Drops.Any()) {
                            item.Vaulted = "true";
                        }

                    }
                    output.Add(item);
                }
            }
            return output;
        }
    }
}
