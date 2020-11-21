namespace WarframeTrackerLib.Config {
    public class ApplicationSecrets {
        public string ConnectionString { get; set; }

        public string PushbulletApiKey { get; set; }
        
        public string DefaultPushbulletDeviceIden { get; set; }

        public string InvasionRewardChannel { get; set; }

        public string TaskTriggerPassphrase { get; set; }

        public string TwitterApiKey { get; set; }

        public string TwitterSecretKey { get; set; }

        public string TwitterBearerToken { get; set; }
    }
}