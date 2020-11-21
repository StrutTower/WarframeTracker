using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;
using WarframeTrackerLib.Config;

namespace WarframeTrackerLib.Utilities {
    public class TwitterHelper {
        private readonly ApplicationSecrets applicationSecrets;

        public TwitterHelper(IOptions<ApplicationSecrets> applicationSecrets) {
            this.applicationSecrets = applicationSecrets.Value;
        }

        public void SendTweet() {
            var client = new TwitterClient(applicationSecrets.TwitterApiKey, applicationSecrets.TwitterSecretKey, applicationSecrets.TwitterBearerToken);
            var username = client.Tweets.PublishTweetAsync("test2").Result;
            Console.WriteLine(username);
        }
    }
}
