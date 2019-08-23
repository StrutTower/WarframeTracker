using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.Config;

namespace WarframeTrackerCron.Utilities {
    public class PushBulletHelper {
        public void SendNotificationToCellPhone(string message) {
            ApplicationSecrets appSecrets = ApplicationSecrets.Get();

            PushbulletClient pbCLient = new PushbulletClient(appSecrets.PushbulletApiKey);

            PushNoteRequest request = new PushNoteRequest {
                ChannelTag = appSecrets.InvasionRewardChannel,
                Title = "Warframe Tracker",
                Body = message
            };

            var response = pbCLient.PushNote(request);
        }
    }
}
