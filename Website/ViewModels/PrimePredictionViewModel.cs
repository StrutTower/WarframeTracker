using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;

namespace Website.ViewModels {
    public class PrimePredictionViewModel {
        public List<WarframeItem> StandardWarframes { get; set; }

        public List<WarframeItem> PrimeWarframes { get; set; }

        public List<WarframeItem> StandardWithoutPrimeWarframes { get; set; }

        public List<WarframeItem> StandardWarframeWithoutReleaseDate { get; set; }

        public DateTime LastPrimeReleaseDate { get; set; }

        public string Gender { get; set; }

        public int PrimeIndex { get; set; }

        public PrimePredictionViewModel Load(UnitOfWork uow, WarframeItemUtilities itemUtils, AppSettings appSettings) {
            List<WarframeItem> frames = itemUtils.GetByCategoryID(appSettings.WarframeItemCategoryID);

            StandardWarframes = frames.Where(x => !x.IsPrime && !x.Name.Contains("Umbra")).OrderBy(x => string.IsNullOrWhiteSpace(x.ReleaseDate)).ThenBy(x => x.ReleaseDate).ToList();
            PrimeWarframes = frames.Where(x => x.IsPrime).OrderBy(x => x.ReleaseDate).ToList();

            WarframeItem lastPrime = PrimeWarframes.Last();
            LastPrimeReleaseDate = DateTime.Parse(lastPrime.ReleaseDate);

            StandardWithoutPrimeWarframes = new List<WarframeItem>();
            foreach (WarframeItem standard in StandardWarframes) {
                WarframeItem primeVariant = PrimeWarframes.SingleOrDefault(x => x.Name == standard.Name + " Prime");
                if (primeVariant == null) {
                    StandardWithoutPrimeWarframes.Add(standard);
                }
            }

            StandardWithoutPrimeWarframes = StandardWithoutPrimeWarframes.OrderBy(x => string.IsNullOrWhiteSpace(x.ReleaseDate)).ThenBy(x => x.ReleaseDate).ToList();

            PrimeIndex = 0;
            foreach (WarframeItem item in PrimeWarframes) {
                PrimeIndex++;
                if (PrimeIndex % 4 == 0)
                    Gender = "Male";
                else if (PrimeIndex % 4 == 2)
                    Gender = "Female";
            }

            return this;
        }
    }
}
