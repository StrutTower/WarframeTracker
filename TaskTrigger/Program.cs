using Serilog;
using Serilog.Events;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using WesternStateHospital.WSHUtilities;

namespace TaskTrigger {
    class Program {
        
        static void Main(string[] args) {
            ILogger log = ConfigLogger();

            string urlArg = args.SingleOrDefault(x => x.StartsWith("-url="));
            //urlArg = "-url=http://tillers.dynu.com/WarframeTracker/Task/Test";
            string passphraseArg = args.SingleOrDefault(x => x.StartsWith("-passphrase="));
            //passphraseArg = "-passphrase=towerframe";

            string url = urlArg.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries).Last();
            string passphrase = passphraseArg.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries).Last();

            string message = new EasyCryptAES().Encrypt(passphrase, DateTime.Now.ToString());

            Uri uri = new Uri(url);
            Uri uriWithQuery = uri.AddQuery("message", message);

            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using HttpClient httpClient = new HttpClient(httpClientHandler);

            HttpResponseMessage response = httpClient.GetAsync(uriWithQuery).Result;
            if (response.IsSuccessStatusCode) {
                log.Information($"{uri} - StatusCode: {(int)response.StatusCode} {response.ReasonPhrase}");
            } else {
                log.Error($"{uri} - StatusCode: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }

        private static ILogger ConfigLogger() {
            return new LoggerConfiguration()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.File(Environment.ExpandEnvironmentVariables(@"%APPDATA%\TaskTrigger\taskTrigger.log"))
                .CreateLogger();
        }
    }
}
