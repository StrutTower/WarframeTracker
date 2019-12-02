using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.Areas.Admin.ViewModels {
    public class EditManualItemDataViewModel {
        public ManualItemData ManualItemData { get; set; }

        public string SelectedItemName { get; set; }

        public List<string> PropertyNames { get; set; }

        public EditManualItemDataViewModel Load(ManualItemData manualItemData, UnitOfWork uow, WarframeItemUtilities itemUtils) {
            ManualItemData = manualItemData;
            if (ManualItemData == null) ManualItemData = new ManualItemData();

            if (!string.IsNullOrWhiteSpace(ManualItemData.ItemUniqueName)) {
                WarframeItem item = itemUtils.GetByUniqueName(ManualItemData.ItemUniqueName);
                SelectedItemName = item.Name;
            } 

            PropertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(WarframeItem).GetProperties()) {
                if (prop.CanRead &&
                    prop.CanWrite &&
                    prop.Name != "UniqueName" &&
                    prop.PropertyType == typeof(string) ||
                    prop.PropertyType == typeof(int?) ||
                    prop.PropertyType == typeof(double?) ||
                    prop.PropertyType == typeof(bool)) {
                    PropertyNames.Add(prop.Name);
                }
            }
            PropertyNames = PropertyNames.OrderBy(x => x).ToList();

            return this;
        }
    }
}
