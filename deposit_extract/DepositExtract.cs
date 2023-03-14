using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace deposit_extract
{
    public class DepositExtract
    {
        [FunctionName("DepositExtract")]
        public void Run ([TimerTrigger("0 0 2 * * *")] TimerInfo myTimer, ILogger log)
        {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            string apiUrl = Environment.GetEnvironmentVariable("API_URL");

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            using (HttpClient client = new HttpClient())
            {
                // Faire la requête HTTP GET et récupérer la réponse
                var url = $"{apiUrl}{apiKey}";
                var response = client.GetAsync($"{apiUrl}{apiKey}");
                var responseString = response.Result.Content.ToString();
                log.Log(LogLevel.Debug, url);
                log.Log(LogLevel.Debug, responseString);
            }
        }
    }
}
