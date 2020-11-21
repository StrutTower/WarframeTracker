using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WarframeTrackerLib.Config;
using WarframeTrackerLib.Services;
using WesternStateHospital.WSHUtilities;

namespace Website.Controllers {
    public class TaskController : Controller {
        private readonly ApplicationSecrets appSecrets;
        private readonly InvasionRewardNotifier invasionRewardNotifier;

        public TaskController(IOptions<ApplicationSecrets> appSecrets, InvasionRewardNotifier invasionRewardNotifier) {
            this.appSecrets = appSecrets.Value;
            this.invasionRewardNotifier = invasionRewardNotifier;
        }

        public IActionResult Test(string message) {
            bool success = false;
            string reason = string.Empty;
            TimeSpan difference = new TimeSpan(0);
            string output = new EasyCryptAES().Decrypt(appSecrets.TaskTriggerPassphrase, message);
            if (DateTime.TryParse(output, out DateTime result)) {
                difference = DateTime.Now.Subtract(result);
                if (Math.Abs(difference.TotalSeconds) < 30) {
                    success = true;
                } else {
                    reason = "outside timespan range";
                }
            } else
                reason = "failed to parse";
            return Json(new {
                success,
                reason,
                message = output,
                difference = difference.TotalSeconds
            });
        }

        public IActionResult InvasionRewardNotifications(string message) {
            string output = new EasyCryptAES().Decrypt(appSecrets.TaskTriggerPassphrase, message);
            if (string.IsNullOrWhiteSpace(message)) {
                return BadRequest("Missing parameter");
            } else {
                if (DateTime.TryParse(output, out DateTime result)) {
                    TimeSpan difference = DateTime.Now.Subtract(result);
                    if (Math.Abs(difference.TotalSeconds) < 30) {
                        invasionRewardNotifier.StartTask();
                        return Ok();
                    } else {
                        return BadRequest("Invalid: " + difference.TotalSeconds);
                    }
                } else {
                    return UnprocessableEntity("Failed to parse message");
                }
            }
        }
    }
}
