using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WarframeTrackerLib.Domain;

namespace WarframeTrackerLib.Utilities {
    public class AdvancedSearchModel : IValidatableObject {
        [Display(Name = "Item Name", Prompt = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Mastery Level Requirement", Prompt = "1, 2, 3, 1-3")]
        public string MasteryRequirement { get; set; }

        [Display(Name = "Codex Section", Prompt = "Codex Section")]
        public CodexSection? CodexSection { get; set; }



        private string _masteryRegexPattern = @"^\d+$|^>\d+$|<\d+$|\d+-\d+$";

        public List<string> GetMasteryRequirementParts() {
            if (string.IsNullOrWhiteSpace(MasteryRequirement)) return null;

            MasteryRequirement = MasteryRequirement.Replace(" ", "");

            string[] parts = MasteryRequirement.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts) {
                if (!Regex.IsMatch(part, _masteryRegexPattern)) {
                    throw new Exception($"Invalid Query Syntax: {part}");
                }
            }

            return parts.ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (string.IsNullOrWhiteSpace(Name) && 
                string.IsNullOrWhiteSpace(MasteryRequirement) &&
                !CodexSection.HasValue) {
                yield return new ValidationResult("You must enter at least one search critria");
            }

            if (!string.IsNullOrWhiteSpace(MasteryRequirement)) {
                MasteryRequirement = MasteryRequirement.Replace(" ", "");
                foreach (string part in MasteryRequirement.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (!Regex.IsMatch(part, _masteryRegexPattern)) {
                        yield return new ValidationResult($"Invalid Query Syntax: {part}", new[] { "MasteryRequirement" });
                    }
                }
            }
        }
    }
}
