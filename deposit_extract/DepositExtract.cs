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
        public void Run ([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            //string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            //string apiUrl = Environment.GetEnvironmentVariable("API_URL");

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            using (HttpClient client = new HttpClient())
            {
                // Ajouter l'en-tête API-KEY à la demande
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("API-KEY", "0d8eedb6-b1a1-467f-bf4a-7057174f1cd9");

                // Faire la requête HTTP GET et récupérer la réponse
                client.GetAsync("https://api.test.deposit.ovh/v1/jobs?job=extract:users");
            }
        }
    }
}
