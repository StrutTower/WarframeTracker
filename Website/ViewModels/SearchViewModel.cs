using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;

namespace Website.ViewModels {
    public class SearchViewModel : IValidatableObject {
        public AdvancedSearchModel AdvancedSearchModel { get; set; }

        public SelectList CodexSectionSelectList { get; set; }

        public SelectList ItemCategorySelectList { get; set; }


        public SearchViewModel Load() {
            Dictionary<int, string> codexSections = new Dictionary<int, string>();
            foreach(var item in Enum.GetValues(typeof(CodexSection))) {
                codexSections.Add((int)item, item.ToString());
            }
            CodexSectionSelectList = new SelectList(codexSections, "Key", "Value");

            List<ItemCategory> itemCategories;
            using(IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                itemCategories = new ItemCategoryRepository(uow).GetAll();
            }
            ItemCategorySelectList = new SelectList(itemCategories, "ID", "Name");

            return this;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (AdvancedSearchModel == null) {
                yield return new ValidationResult("You must select at least one option");
            }
        }
    }
}
