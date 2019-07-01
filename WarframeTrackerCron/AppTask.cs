using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerCron.Tasks;

namespace WarframeTrackerCron {
    public class AppTask {
        public AppTask(Type type, string description) {
            Type = type;
            Description = description;
        }

        public Type Type { get; set; }

        public string Description { get; set; }

        public static Dictionary<string, AppTask> GetTasks() {
            return new Dictionary<string, AppTask>(StringComparer.InvariantCultureIgnoreCase) {
                {
                    "-trackInvasionRewards", new AppTask(typeof(StoreInvasionRewardData), "Stores any current invasion rewards in the TrackedData table.")
                },
                {
                    "-invasionRewardNotification", new AppTask(typeof(InvasionRewardNotification), "")
                }
            };
        }
    }
}
