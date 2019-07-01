using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.WarframeApi;

namespace Website.ViewModels {
    public class EditManualItemDataViewModel {
        public ManualItemData ManualItemData { get; set; }

        public string SelectedItemName { get; set; }

        public List<string> PropertyNames { get; set; }

        public EditManualItemDataViewModel Load(ManualItemData manualItemData, IUnitOfWork uow) {
            ManualItemData = manualItemData;
            if (ManualItemData == null) ManualItemData = new ManualItemData();

            if (!string.IsNullOrWhiteSpace(ManualItemData.ItemUniqueName)) {
                WarframeItem item = new WarframeItemUtilities(uow).GetByUniqueName(ManualItemData.ItemUniqueName);
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
